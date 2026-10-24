using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VkSagittarius.Core;
using Xunit;

namespace VkSagittarius.Tests;

public class DaemonIpcTests
{
    private static string TempSocket() =>
        Path.Combine(Path.GetTempPath(), $"vksag-test-{Guid.NewGuid():N}.sock");

    [Fact]
    public async Task DaemonServesStatusAndAcceptsCommands()
    {
        var socket = TempSocket();
        var boot = DeviceFactory.Demo("测试");
        using var server = new DaemonServer(boot, socket, persistLog: false);
        server.Start();

        try
        {
            using var client = new DaemonClient(socket);
            Assert.True(await client.ConnectAsync(TimeSpan.FromSeconds(2)), "客户端连不上守护进程");

            var status = await client.StatusAsync(TimeSpan.FromSeconds(2));
            Assert.NotNull(status);
            Assert.True(status!.Ok, status.Error);
            Assert.Equal(2, status.Status!.Devices.Length);
            Assert.True(status.Status.Demo);

            var led = await client.SendAsync(new IpcRequest
            {
                Op = Ipc.OpLed,
                Led = new LedRequest(6, 0, 80, Proto.LedSpeedFast, 255, 0, 128),
            });
            Assert.True(led.Ok, led.Error);

            var fan = await client.SendAsync(new IpcRequest
            {
                Op = Ipc.OpFan,
                Fan = new FanRequest(2, Proto.FanModeCustom, [20, 55, 100], [45, 65, 85]),
            });
            Assert.True(fan.Ok, fan.Error);

            var pause = await client.SendAsync(new IpcRequest { Op = Ipc.OpPause });
            Assert.True(pause.Ok, pause.Error);

            // 心跳暂停后帧数不再变化
            var before = (await client.StatusAsync())!.Status!.Frames;
            await Task.Delay(600);
            var after = (await client.StatusAsync())!.Status!.Frames;
            Assert.Equal(before, after);
        }
        finally
        {
            server.Dispose();
            if (File.Exists(socket)) File.Delete(socket);
        }
    }

    [Fact]
    public async Task SecondDaemonOnSameSocketIsRejected()
    {
        var socket = TempSocket();
        var first = new DaemonServer(DeviceFactory.Demo("测试1"), socket, persistLog: false);
        first.Start();
        try
        {
            var second = new DaemonServer(DeviceFactory.Demo("测试2"), socket, persistLog: false);
            Assert.Throws<InvalidOperationException>(() => second.Start());
            second.Dispose();
        }
        finally
        {
            first.Dispose();
            if (File.Exists(socket)) File.Delete(socket);
        }
    }

    [Fact]
    public async Task StatusEventPushesToConnectedClient()
    {
        var socket = TempSocket();
        using var server = new DaemonServer(DeviceFactory.Demo("测试"), socket, persistLog: false);
        server.Start();
        try
        {
            using var client = new DaemonClient(socket);
            Assert.True(await client.ConnectAsync(TimeSpan.FromSeconds(2)));

            var tcs = new TaskCompletionSource<StatusDto>(TaskCreationOptions.RunContinuationsAsynchronously);
            client.StatusReceived += s => tcs.TrySetResult(s);
            await client.StatusAsync();                        // 触发一次连接

            var done = await Task.WhenAny(tcs.Task, Task.Delay(2500));
            Assert.Same(tcs.Task, done);
            var pushed = await tcs.Task;
            Assert.True(pushed.Devices.Length >= 1);
        }
        finally
        {
            server.Dispose();
            if (File.Exists(socket)) File.Delete(socket);
        }
    }
}
