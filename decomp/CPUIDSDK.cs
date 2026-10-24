using System;
using System.Runtime.InteropServices;

public class CPUIDSDK
{
	private enum PTR : uint
	{
		PTR0 = 834560892u,
		PTR1 = 3385693083u,
		PTR2 = 1663485517u,
		PTR3 = 3286337475u,
		PTR4 = 1183878433u,
		PTR5 = 1993797037u,
		PTR6 = 2412191630u,
		PTR7 = 1918952641u,
		PTR8 = 3681662691u,
		PTR9 = 3859532823u,
		PTR10 = 320874048u,
		PTR11 = 1374331861u,
		PTR12 = 3495534771u,
		PTR13 = 3390280743u,
		PTR14 = 2060907949u,
		PTR15 = 1875828637u,
		PTR16 = 1583529157u,
		PTR17 = 1594932769u,
		PTR18 = 1597161061u,
		PTR19 = 3225387135u,
		PTR20 = 338045004u,
		PTR21 = 1053719964u,
		PTR22 = 1122665941u,
		PTR23 = 3714431691u,
		PTR24 = 3159193754u,
		PTR25 = 266346432u,
		PTR26 = 1194102361u,
		PTR27 = 122687136u,
		PTR28 = 1097106121u,
		PTR29 = 1512485965u,
		PTR30 = 3484917615u,
		PTR31 = 3356200983u,
		PTR32 = 2575119098u,
		PTR33 = 480262464u,
		PTR34 = 354167352u,
		PTR35 = 3749166831u,
		PTR36 = 3908817399u,
		PTR37 = 76417308u,
		PTR38 = 2554933394u,
		PTR39 = 4048544415u,
		PTR40 = 1449307333u,
		PTR41 = 2075064157u,
		PTR42 = 1104184225u,
		PTR43 = 1215467749u,
		PTR44 = 2902153718u,
		PTR45 = 3363279087u,
		PTR46 = 4153536291u,
		PTR47 = 3318451095u,
		PTR48 = 1565702821u,
		PTR49 = 3105190442u,
		PTR50 = 3823355847u,
		PTR51 = 3845245539u,
		PTR52 = 1189252549u,
		PTR53 = 644500692u,
		PTR54 = 3711023715u,
		PTR55 = 3550193463u,
		PTR56 = 551567808u,
		PTR57 = 1779094549u,
		PTR58 = 3232203087u,
		PTR59 = 3480723183u,
		PTR60 = 998012664u,
		PTR61 = 3951941403u,
		PTR62 = 1686948121u,
		PTR63 = 1952770249u,
		PTR64 = 250748388u,
		PTR65 = 1282840813u,
		PTR66 = 3961116723u,
		PTR67 = 1376035849u,
		PTR68 = 4042514919u,
		PTR69 = 2878953266u,
		PTR70 = 767318904u,
		PTR71 = 1490203045u,
		PTR72 = 1411164217u,
		PTR73 = 188749440u,
		PTR74 = 4018790163u,
		PTR75 = 1755500869u,
		PTR76 = 2727298334u,
		PTR77 = 4232312967u,
		PTR78 = 276439284u,
		PTR79 = 350366148u,
		PTR80 = 1363583629u,
		PTR81 = 700076916u,
		PTR82 = 3612061335u,
		PTR83 = 2827440398u,
		PTR84 = 2276659046u,
		PTR85 = 1789187401u,
		PTR86 = 1595194921u,
		PTR87 = 1877270473u,
		PTR88 = 1509209065u,
		PTR89 = 1290443221u,
		PTR90 = 2281902086u,
		PTR91 = 3813787299u,
		PTR92 = 3312290523u,
		PTR93 = 3731864799u,
		PTR94 = 1656014185u,
		PTR95 = 4030324851u,
		PTR96 = 631262016u,
		PTR97 = 520633872u,
		PTR98 = 4066239675u,
		PTR99 = 3122885702u,
		PTR100 = 1384686865u,
		PTR101 = 4064535687u,
		PTR102 = 2244545426u,
		PTR103 = 1083605293u,
		PTR104 = 1336319821u,
		PTR105 = 211556664u,
		PTR106 = 1777652713u,
		PTR107 = 1347592357u,
		PTR108 = 1813174309u,
		PTR109 = 3391067199u,
		PTR110 = 546193692u,
		PTR111 = 3454901211u,
		PTR112 = 2314408934u,
		PTR113 = 2620995698u,
		PTR114 = 2203125410u,
		PTR115 = 1901650609u,
		PTR116 = 2056189213u,
		PTR117 = 3736190307u,
		PTR118 = 2148597794u,
		PTR119 = 1327930957u,
		PTR120 = 3738156447u,
		PTR121 = 2089613593u,
		PTR122 = 1545517117u,
		PTR123 = 782130492u,
		PTR124 = 1609351129u,
		PTR125 = 767581056u,
		PTR126 = 240000156u,
		PTR127 = 3492913251u,
		PTR128 = 1876615093u,
		PTR129 = 3833579775u,
		PTR130 = 981759240u,
		PTR131 = 3264841011u,
		PTR132 = 1708182433u,
		PTR133 = 3739205055u,
		PTR134 = 182719944u,
		PTR135 = 4218943215u,
		PTR136 = 3620056971u,
		PTR137 = 3196681490u,
		PTR138 = 1327275577u,
		PTR139 = 1435151125u,
		PTR140 = 1388881297u,
		PTR141 = 1184795965u,
		PTR142 = 2389384406u,
		PTR143 = 27132732u,
		PTR144 = 3365769531u,
		PTR145 = 1334353681u,
		PTR146 = 2952224750u,
		PTR147 = 3356463135u,
		PTR148 = 1314954433u,
		PTR149 = 3702634851u,
		PTR150 = 4178440731u,
		PTR151 = 4121946975u,
		PTR152 = 356919948u,
		PTR153 = 3816933123u,
		PTR154 = 1000109880u,
		PTR155 = 1430301313u,
		PTR156 = 3567626571u,
		PTR157 = 3710106183u,
		PTR158 = 2480088998u,
		PTR159 = 2133917281u,
		PTR160 = 533348244u,
		PTR161 = 2851296230u,
		PTR162 = 103943268u,
		PTR163 = 2241792830u,
		PTR164 = 1998515773u,
		PTR165 = 3537741243u,
		PTR166 = 278405424u,
		PTR167 = 4047102579u,
		PTR168 = 3862154343u,
		PTR169 = 3382809411u,
		PTR170 = 848717100u,
		PTR171 = 1444981825u,
		PTR172 = 2593731890u,
		PTR173 = 437793840u,
		PTR174 = 1374594013u,
		PTR175 = 2361203066u,
		PTR176 = 2418614354u,
		PTR177 = 1356374449u,
		PTR178 = 2998625654u,
		PTR179 = 1170246529u,
		PTR180 = 1415489725u,
		PTR181 = 2795851082u,
		PTR182 = 1862458885u,
		PTR183 = 147329424u,
		PTR184 = 1385997625u,
		PTR185 = 2792967410u,
		PTR186 = 1269995365u,
		PTR187 = 519585264u,
		PTR188 = 2179924958u,
		PTR189 = 3333131607u,
		PTR190 = 1059356232u,
		PTR191 = 607537260u,
		PTR192 = 1371186037u,
		PTR193 = 3475217991u,
		PTR194 = 2358581546u,
		PTR195 = 792485496u,
		PTR196 = 3837118827u,
		PTR197 = 1158187537u,
		PTR198 = 1413392509u,
		PTR199 = 2750629862u,
		PTR200 = 2212825034u,
		PTR201 = 1751961817u,
		PTR202 = 1164872413u,
		PTR203 = 2847757178u,
		PTR204 = 3667637559u,
		PTR205 = 3035064782u,
		PTR206 = 1013217480u,
		PTR207 = 3089199170u,
		PTR208 = 1873993573u,
		PTR209 = 4212782643u,
		PTR210 = 2771995250u,
		PTR211 = 2690072750u,
		PTR212 = 3409024611u,
		PTR213 = 123735744u,
		PTR214 = 2467505702u,
		PTR215 = 3913405059u,
		PTR216 = 489699936u,
		PTR217 = 1428466249u,
		PTR218 = 30147480u,
		PTR219 = 920808900u,
		PTR220 = 2622175382u,
		PTR221 = 3692935227u,
		PTR222 = 2468161082u,
		PTR223 = 395325216u,
		PTR224 = 1241551873u,
		PTR225 = 772824096u,
		PTR226 = 832463676u,
		PTR227 = 3360395415u,
		PTR228 = 2110978981u,
		PTR229 = 1217958193u,
		PTR230 = 645156072u,
		PTR231 = 3643912803u,
		PTR232 = 579224844u,
		PTR233 = 225712872u,
		PTR234 = 4130466915u,
		PTR235 = 1826675137u,
		PTR236 = 1776210877u,
		PTR237 = 1452059929u,
		PTR238 = 4071744867u,
		PTR239 = 1489416589u,
		PTR240 = 1421650297u,
		PTR241 = 1585102069u,
		PTR242 = 815030568u,
		PTR243 = 1630061137u,
		PTR244 = 852387228u,
		PTR245 = 3232989543u,
		PTR246 = 1649329309u,
		PTR247 = 3353579463u,
		PTR248 = 2186609834u,
		PTR249 = 1888674085u,
		PTR250 = 484325820u,
		PTR251 = 1323343297u,
		PTR252 = 3572607459u,
		PTR253 = 1757860237u,
		PTR254 = 1310366773u,
		PTR255 = 3386086311u,
		PTR256 = 1611710497u,
		PTR257 = 2086860997u,
		PTR258 = 1674495901u,
		PTR259 = 1901126305u,
		PTR260 = 3642339891u,
		PTR261 = 116919792u,
		PTR262 = 230824836u,
		PTR263 = 363211596u,
		PTR264 = 2650749950u,
		PTR265 = 3914584743u,
		PTR266 = 2699510222u,
		PTR267 = 1994845645u,
		PTR268 = 4138069323u,
		PTR269 = 282730932u,
		PTR270 = 4086425379u,
		PTR271 = 2873841302u,
		PTR272 = 3931493547u,
		PTR273 = 3344404143u,
		PTR274 = 3151067042u,
		PTR275 = 1369350973u,
		PTR276 = 337651776u,
		PTR277 = 3711285867u,
		PTR278 = 4126534635u,
		PTR279 = 624577140u,
		PTR280 = 3705125295u,
		PTR281 = 374615208u,
		PTR282 = 881879328u,
		PTR283 = 160174872u,
		PTR284 = 3328150719u,
		PTR285 = 3497763063u,
		PTR286 = 2145583045u,
		PTR287 = 1347985585u,
		PTR288 = 2034168445u,
		PTR289 = 1435675429u,
		PTR290 = 799039296u,
		PTR291 = 2032595533u,
		PTR292 = 3903312207u,
		PTR293 = 2182808630u,
		PTR294 = 3458178111u,
		PTR295 = 1392551425u,
		PTR296 = 1696516669u,
		PTR297 = 847144188u,
		PTR298 = 3561465999u,
		PTR299 = 3021301802u,
		PTR300 = 3462503619u,
		PTR301 = 4200068271u,
		PTR302 = 3801597231u,
		PTR303 = 142610688u,
		PTR304 = 786324924u,
		PTR305 = 2458985762u,
		PTR306 = 3177937622u,
		PTR307 = 2316506150u,
		PTR308 = 4016037567u,
		PTR309 = 1600831189u,
		PTR310 = 1336188745u,
		PTR311 = 3524633643u,
		PTR312 = 284565996u,
		PTR313 = 4252629747u,
		PTR314 = 623266380u,
		PTR315 = 3980253819u,
		PTR316 = 3767910699u,
		PTR317 = 1589034349u,
		PTR318 = 4097173611u,
		PTR319 = 287580744u,
		PTR320 = 2191328570u,
		PTR321 = 2713666430u,
		PTR322 = 2162885078u,
		PTR323 = 846619884u,
		PTR324 = 3263399175u,
		PTR325 = 1420601689u,
		PTR326 = 2321749190u,
		PTR327 = 3631460583u,
		PTR328 = 4187222823u,
		PTR329 = 417214908u,
		PTR330 = 86248008u,
		PTR331 = 1308138481u,
		PTR332 = 47318436u,
		PTR333 = 3572869611u,
		PTR334 = 437138460u,
		PTR335 = 1297783477u,
		PTR336 = 3688347567u,
		PTR337 = 233708508u,
		PTR338 = 205396092u,
		PTR339 = 123997896u,
		PTR340 = 1295161957u,
		PTR341 = 3502088571u,
		PTR342 = 1638187849u,
		PTR343 = 2212300730u,
		PTR344 = 1171557289u,
		PTR345 = 294658848u,
		PTR346 = 1897718329u,
		PTR347 = 2798603678u,
		PTR348 = 3194059970u,
		PTR349 = 2950520762u,
		PTR350 = 761813712u,
		PTR351 = 2649308114u,
		PTR352 = 1749602449u,
		PTR353 = 1925768593u,
		PTR354 = 3820472175u,
		PTR355 = 3218964410u,
		PTR356 = 3950630643u,
		PTR357 = 935358336u,
		PTR358 = 1373021101u,
		PTR359 = 971404236u,
		PTR360 = 629033724u,
		PTR361 = 1260033589u,
		PTR362 = 1150716205u,
		PTR363 = 4272684375u,
		PTR364 = 1069842312u,
		PTR365 = 3348336423u,
		PTR366 = 2311000958u,
		PTR367 = 3599084811u,
		PTR368 = 4237424931u,
		PTR369 = 1993010581u,
		PTR370 = 2549297126u,
		PTR371 = 4273864059u,
		PTR372 = 93457188u,
		PTR373 = 2753644610u,
		PTR374 = 4040024475u,
		PTR375 = 2929679678u,
		PTR376 = 1803081457u,
		PTR377 = 1972038421u,
		PTR378 = 1477095445u,
		PTR379 = 2819051534u,
		PTR380 = 3361575099u,
		PTR381 = 3669734775u,
		PTR382 = 2707636934u,
		PTR383 = 3574704675u
	}

	private delegate IntPtr CPUIDSDK_fp_CreateInstance();

	private delegate void CPUIDSDK_fp_DestroyInstance(IntPtr objptr);

	private delegate int CPUIDSDK_fp_Init(IntPtr objptr, string _szDllPath, string _szDllFilename, int _config_flag, ref int _errorcode, ref int _extended_errorcode);

	private delegate void CPUIDSDK_fp_Close(IntPtr objptr);

	private delegate void CPUIDSDK_fp_RefreshInformation(IntPtr objptr);

	private delegate void CPUIDSDK_fp_GetDllVersion(IntPtr objptr, ref int _version);

	private delegate int CPUIDSDK_fp_GetNbProcessors(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetProcessorFamily(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetProcessorCoreSetCount(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetProcessorCoreCount(IntPtr objptr, int _proc_index, int _core_set);

	private delegate int CPUIDSDK_fp_GetProcessorThreadCount(IntPtr objptr, int _proc_index, int _core_set);

	private delegate int CPUIDSDK_fp_GetProcessorCoreSetType(IntPtr objptr, int _proc_index, int _core_set);

	private delegate int CPUIDSDK_fp_GetProcessorCoreThreadCount(IntPtr objptr, int _proc_index, int _core_index);

	private delegate int CPUIDSDK_fp_GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index);

	private delegate IntPtr CPUIDSDK_fp_GetProcessorName(IntPtr objptr, int _proc_index);

	private delegate IntPtr CPUIDSDK_fp_GetProcessorCodeName(IntPtr objptr, int _proc_index);

	private delegate IntPtr CPUIDSDK_fp_GetProcessorSpecification(IntPtr objptr, int _proc_index);

	private delegate IntPtr CPUIDSDK_fp_GetProcessorPackage(IntPtr objptr, int _proc_index);

	private delegate IntPtr CPUIDSDK_fp_GetProcessorStepping(IntPtr objptr, int _proc_index);

	private delegate float CPUIDSDK_fp_GetProcessorTDP(IntPtr objptr, int _proc_index);

	private delegate float CPUIDSDK_fp_GetProcessorManufacturingProcess(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_IsProcessorInstructionSetAvailable(IntPtr objptr, int _proc_index, int _iset);

	private delegate float CPUIDSDK_fp_GetProcessorCoreClockFrequency(IntPtr objptr, int _proc_index, int _core_index);

	private delegate float CPUIDSDK_fp_GetProcessorCoreClockMultiplier(IntPtr objptr, int _proc_index, int _core_index);

	private delegate float CPUIDSDK_fp_GetProcessorCoreTemperature(IntPtr objptr, int _proc_index, int _core_index);

	private delegate float CPUIDSDK_fp_GetBusFrequency(IntPtr objptr);

	private delegate float CPUIDSDK_fp_GetProcessorRatedBusFrequency(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetProcessorStockClockFrequency(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetProcessorStockBusFrequency(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetProcessorMaxCacheLevel(IntPtr objptr, int _proc_index);

	private delegate void CPUIDSDK_fp_GetProcessorCacheParameters(IntPtr objptr, int _proc_index, int _core_set, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size);

	private delegate int CPUIDSDK_fp_GetProcessorID(IntPtr objptr, int _proc_index);

	private delegate float CPUIDSDK_fp_GetProcessorVoltageID(IntPtr objptr, int _proc_index);

	private delegate int CPUIDSDK_fp_GetMemoryType(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemorySize(IntPtr objptr);

	private delegate float CPUIDSDK_fp_GetMemoryClockFrequency(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryNumberOfChannels(IntPtr objptr);

	private delegate float CPUIDSDK_fp_GetMemoryCASLatency(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryRAStoCASDelay(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryRASPrecharge(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryTRAS(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryTRC(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryCommandRate(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeVendor(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeModel(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeRevision(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeVendor(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeModel(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeRevision(IntPtr objptr);

	private delegate void CPUIDSDK_fp_GetGraphicBusLinkParameters(IntPtr objptr, ref int _bus_type, ref int _link_width);

	private delegate void CPUIDSDK_fp_GetMemorySlotsConfig(IntPtr objptr, ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize);

	private delegate IntPtr CPUIDSDK_fp_GetBIOSVendor(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetBIOSVersion(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetBIOSDate(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetMainboardVendor(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetMainboardModel(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetMainboardRevision(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetMainboardSerialNumber(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemManufacturer(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemProductName(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemVersion(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemSerialNumber(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemUUID(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemSKU(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetSystemFamily(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetChassisManufacturer(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetChassisType(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetChassisSerialNumber(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryInfosExt(IntPtr objptr, ref IntPtr _szLocation, ref IntPtr _szUsage, ref IntPtr _szCorrection);

	private delegate int CPUIDSDK_fp_GetNumberOfMemoryDevices(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryDeviceInfos(IntPtr objptr, int _device_index, ref int _size, ref IntPtr _szFormat);

	private delegate int CPUIDSDK_fp_GetMemoryDeviceInfosExt(IntPtr objptr, int _device_index, ref IntPtr _szDesignation, ref IntPtr _szType, ref int _total_width, ref int _data_width, ref int _speed);

	private delegate int CPUIDSDK_fp_GetProcessorSockets(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryMaxCapacity(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetMemoryMaxNumberOfDevices(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetNumberOfSPDModules(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetSPDModuleType(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleSize(IntPtr objptr, int _spd_index);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModuleFormat(IntPtr objptr, int _spd_index);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModuleManufacturer(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleManufacturerID(IntPtr objptr, int _spd_index, byte[] _id);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModuleDRAMManufacturer(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleMaxFrequency(IntPtr objptr, int _spd_index);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSpecification(IntPtr objptr, int _spd_index);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModulePartNumber(IntPtr objptr, int _spd_index);

	private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSerialNumber(IntPtr objptr, int _spd_index);

	private delegate float CPUIDSDK_fp_GetSPDModuleMinTRCD(IntPtr objptr, int _spd_index);

	private delegate float CPUIDSDK_fp_GetSPDModuleMinTRP(IntPtr objptr, int _spd_index);

	private delegate float CPUIDSDK_fp_GetSPDModuleMinTRAS(IntPtr objptr, int _spd_index);

	private delegate float CPUIDSDK_fp_GetSPDModuleMinTRC(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleManufacturingDate(IntPtr objptr, int _spd_index, ref int _year, ref int _week);

	private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfBanks(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleDataWidth(IntPtr objptr, int _spd_index);

	private delegate float CPUIDSDK_fp_GetSPDModuleTemperature(IntPtr objptr, int _spd_index);

	private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfProfiles(IntPtr objptr, int _spd_index);

	private delegate void CPUIDSDK_fp_GetSPDModuleProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd);

	private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles(IntPtr objptr, int _spd_index, ref int _epp_revision);

	private delegate void CPUIDSDK_fp_GetSPDModuleEPPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd);

	private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles(IntPtr objptr, int _spd_index, ref int _xmp_revision);

	private delegate int CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL(IntPtr objptr, int _spd_index, int _profile_index);

	private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos(IntPtr objptr, int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL);

	private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL);

	private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles(IntPtr objptr, int _spd_index, ref int _amp_revision);

	private delegate void CPUIDSDK_fp_GetSPDModuleAMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC);

	private delegate int CPUIDSDK_fp_GetSPDModuleRawData(IntPtr objptr, int _spd_index, int _offset);

	private delegate int CPUIDSDK_fp_GetNumberOfDisplayAdapter(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterID(IntPtr objptr, int _adapter_index);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterName(IntPtr objptr, int _adapter_index);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCodeName(IntPtr objptr, int _adapter_index);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels(IntPtr objptr, int _adapter_index);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel(IntPtr objptr, int _adapter_index);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName(IntPtr objptr, int _adapter_index, int _perf_level);

	private delegate float CPUIDSDK_fp_GetDisplayAdapterClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);

	private delegate float CPUIDSDK_fp_GetDisplayAdapterStockClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);

	private delegate float CPUIDSDK_fp_GetDisplayAdapterTemperature(IntPtr objptr, int _adapter_index, int _domain);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterFanSpeed(IntPtr objptr, int _adapter_index);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterFanPWM(IntPtr objptr, int _adapter_index);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryType(IntPtr objptr, int _adapter_index, ref int _type);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterMemorySize(IntPtr objptr, int _adapter_index, ref int _size);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth(IntPtr objptr, int _adapter_index, ref int _bus_width);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterMemoryVendor(IntPtr objptr, int _adapter_index);

	private delegate IntPtr CPUIDSDK_fp_GetDirectXVersion(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetDisplayAdapterBusInfos(IntPtr objptr, int _adapter_index, ref int _bus_type, ref int _multi_vpu);

	private delegate float CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess(IntPtr objptr, int _adapter_index);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCoreFamily(IntPtr objptr, int _adapter_index, ref int _core);

	private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterDriverVersion(IntPtr objptr, int _adapter_index);

	private delegate int CPUIDSDK_fp_GetNumberOfMonitors(IntPtr objptr);

	private delegate IntPtr CPUIDSDK_fp_GetMonitorName(IntPtr objptr, int _monitor_index);

	private delegate IntPtr CPUIDSDK_fp_GetMonitorVendor(IntPtr objptr, int _monitor_index);

	private delegate IntPtr CPUIDSDK_fp_GetMonitorID(IntPtr objptr, int _monitor_index);

	private delegate IntPtr CPUIDSDK_fp_GetMonitorSerial(IntPtr objptr, int _monitor_index);

	private delegate int CPUIDSDK_fp_GetMonitorManufacturingDate(IntPtr objptr, int _monitor_index, ref int _week, ref int _year);

	private delegate float CPUIDSDK_fp_GetMonitorSize(IntPtr objptr, int _monitor_index);

	private delegate int CPUIDSDK_fp_GetMonitorResolution(IntPtr objptr, int _monitor_index, ref int _width, ref int _height, ref int _frequency);

	private delegate int CPUIDSDK_fp_GetMonitorMaxPixelClock(IntPtr objptr, int _monitor_index);

	private delegate float CPUIDSDK_fp_GetMonitorGamma(IntPtr objptr, int _monitor_index);

	private delegate int CPUIDSDK_fp_GetNumberOfStorageDevice(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetStorageDriveNumber(IntPtr objptr, int _index);

	private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceName(IntPtr objptr, int _index);

	private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceRevision(IntPtr objptr, int _index);

	private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceSerialNumber(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetStorageDeviceBusType(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetStorageDeviceRotationSpeed(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetStorageDeviceFeatureFlag(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes(IntPtr objptr, int _index);

	private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceVolumeLetter(IntPtr objptr, int _index, int _volume_index);

	private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity(IntPtr objptr, int _index, int _volume_index);

	private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity(IntPtr objptr, int _index, int _volume_index);

	private delegate int CPUIDSDK_fp_GetStorageDeviceSmartAttribute(IntPtr objptr, int _index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data);

	private delegate int CPUIDSDK_fp_GetStorageDevicePowerOnHours(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetStorageDevicePowerCycleCount(IntPtr objptr, int _index);

	private delegate float CPUIDSDK_fp_GetStorageDeviceTotalCapacity(IntPtr objptr, int _index);

	private delegate int CPUIDSDK_fp_GetNumberOfDevices(IntPtr objptr);

	private delegate int CPUIDSDK_fp_GetDeviceClass(IntPtr objptr, int _device_index);

	private delegate IntPtr CPUIDSDK_fp_GetDeviceName(IntPtr objptr, int _device_index);

	private delegate int CPUIDSDK_fp_GetNumberOfSensors(IntPtr objptr, int _device_index, int _sensor_class);

	private delegate int CPUIDSDK_fp_GetSensorInfos(IntPtr objptr, int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref IntPtr _szNamePtr, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value);

	private delegate void CPUIDSDK_fp_SensorClearMinMax(IntPtr objptr, int _device_index, int _sensor_index, int _sensor_class);

	private delegate float CPUIDSDK_fp_GetSensorTypeValue(IntPtr objptr, int _sensor_type, ref int _device_index, ref int _sensor_index);

	public const uint CPUIDSDK_ERROR_NO_ERROR = 0u;

	public const uint CPUIDSDK_ERROR_EVALUATION = 1u;

	public const uint CPUIDSDK_ERROR_DRIVER = 2u;

	public const uint CPUIDSDK_ERROR_VM_RUNNING = 4u;

	public const uint CPUIDSDK_ERROR_LOCKED = 8u;

	public const uint CPUIDSDK_ERROR_INVALID_DLL = 16u;

	public const uint CPUIDSDK_EXT_ERROR_EVAL_1 = 1u;

	public const uint CPUIDSDK_EXT_ERROR_EVAL_2 = 2u;

	public const uint CPUIDSDK_CONFIG_USE_SOFTWARE = 2u;

	public const uint CPUIDSDK_CONFIG_USE_DMI = 4u;

	public const uint CPUIDSDK_CONFIG_USE_PCI = 8u;

	public const uint CPUIDSDK_CONFIG_USE_ACPI = 16u;

	public const uint CPUIDSDK_CONFIG_USE_CHIPSET = 32u;

	public const uint CPUIDSDK_CONFIG_USE_SMBUS = 64u;

	public const uint CPUIDSDK_CONFIG_USE_SPD = 128u;

	public const uint CPUIDSDK_CONFIG_USE_STORAGE = 256u;

	public const uint CPUIDSDK_CONFIG_USE_GRAPHICS = 512u;

	public const uint CPUIDSDK_CONFIG_USE_HWMONITORING = 1024u;

	public const uint CPUIDSDK_CONFIG_USE_PROCESSOR = 2048u;

	public const uint CPUIDSDK_CONFIG_USE_DISPLAY_API = 4096u;

	public const uint CPUIDSDK_CONFIG_USE_ACPI_TIMER = 16384u;

	public const uint CPUIDSDK_CONFIG_UPDATE_PROCESSOR = 65536u;

	public const uint CPUIDSDK_CONFIG_UPDATE_GRAPHICS = 131072u;

	public const uint CPUIDSDK_CONFIG_UPDATE_STORAGE = 262144u;

	public const uint CPUIDSDK_CONFIG_UPDATE_LPCIO = 524288u;

	public const uint CPUIDSDK_CONFIG_UPDATE_DRAM = 1048576u;

	public const uint CPUIDSDK_CONFIG_CHECK_VM = 16777216u;

	public const uint CPUIDSDK_CONFIG_WAKEUP_HDD = 33554432u;

	public const uint CPUIDSDK_CONFIG_SERVER_SAFE = 2147483648u;

	public const uint CPUIDSDK_CONFIG_USE_EVERYTHING = 2147483647u;

	public static int I_UNDEFINED_VALUE = -1;

	public static float F_UNDEFINED_VALUE = -1f;

	public static uint MAX_INTEGER = uint.MaxValue;

	public static float MAX_FLOAT = MAX_INTEGER;

	public const uint CLASS_DEVICE_UNKNOWN = 0u;

	public const uint CLASS_DEVICE_PCI = 1u;

	public const uint CLASS_DEVICE_SMBUS = 2u;

	public const uint CLASS_DEVICE_PROCESSOR = 4u;

	public const uint CLASS_DEVICE_LPCIO = 8u;

	public const uint CLASS_DEVICE_DRIVE = 16u;

	public const uint CLASS_DEVICE_DISPLAY_ADAPTER = 32u;

	public const uint CLASS_DEVICE_HID = 64u;

	public const uint CLASS_DEVICE_BATTERY = 128u;

	public const uint CLASS_DEVICE_EVBOT = 256u;

	public const uint CLASS_DEVICE_NETWORK = 512u;

	public const uint CLASS_DEVICE_MAINBOARD = 1024u;

	public const uint CLASS_DEVICE_MEMORY_MODULE = 2048u;

	public const uint CLASS_DEVICE_PSU = 4096u;

	public const uint CLASS_DEVICE_TYPE_MASK = 2147483647u;

	public const uint CLASS_DEVICE_COMPOSITE = 2147483648u;

	public const uint CPU_MANUFACTURER_MASK = 4278190080u;

	public const uint CPU_FAMILY_MASK = 4294967040u;

	public const uint CPU_MODEL_MASK = uint.MaxValue;

	public const uint CPU_UNKNOWN = 0u;

	public const uint CPU_INTEL = 16777216u;

	public const uint CPU_AMD = 33554432u;

	public const uint CPU_CYRIX = 67108864u;

	public const uint CPU_VIA = 134217728u;

	public const uint CPU_TRANSMETA = 268435456u;

	public const uint CPU_DMP = 536870912u;

	public const uint CPU_UMC = 1073741824u;

	public const uint CPU_IBM = 2181038080u;

	public const uint CPU_QUALCOMM = 2197815296u;

	public const uint CPU_HYGON = 2214592512u;

	public const uint CPU_INTEL_386 = 16777472u;

	public const uint CPU_INTEL_486 = 16777728u;

	public const uint CPU_INTEL_P5 = 16778240u;

	public const uint CPU_INTEL_P6 = 16779264u;

	public const uint CPU_INTEL_NETBURST = 16781312u;

	public const uint CPU_INTEL_MOBILE = 16785408u;

	public const uint CPU_INTEL_CORE = 16793600u;

	public const uint CPU_INTEL_CORE_2 = 16809984u;

	public const uint CPU_INTEL_BONNELL = 16842752u;

	public const uint CPU_INTEL_SALTWELL = 16843008u;

	public const uint CPU_INTEL_SILVERMONT = 16843264u;

	public const uint CPU_INTEL_GOLDMONT = 16843776u;

	public const uint CPU_INTEL_NEHALEM = 16908288u;

	public const uint CPU_INTEL_SANDY_BRIDGE = 16908544u;

	public const uint CPU_INTEL_HASWELL = 16908800u;

	public const uint CPU_INTEL_SKYLAKE = 17039360u;

	public const uint CPU_INTEL_ITANIUM = 17825792u;

	public const uint CPU_INTEL_ITANIUM_2 = 17826048u;

	public const uint CPU_INTEL_MIC = 18874368u;

	public const uint CPU_PENTIUM = 16778241u;

	public const uint CPU_PENTIUM_MMX = 16778242u;

	public const uint CPU_PENTIUM_PRO = 16779265u;

	public const uint CPU_PENTIUM_2 = 16779266u;

	public const uint CPU_PENTIUM_2_M = 16779267u;

	public const uint CPU_CELERON_P2 = 16779268u;

	public const uint CPU_XEON_P2 = 16779269u;

	public const uint CPU_PENTIUM_3 = 16779270u;

	public const uint CPU_PENTIUM_3_M = 16779271u;

	public const uint CPU_PENTIUM_3_S = 16779272u;

	public const uint CPU_CELERON_P3 = 16779273u;

	public const uint CPU_XEON_P3 = 16779274u;

	public const uint CPU_PENTIUM_4 = 16781313u;

	public const uint CPU_PENTIUM_4_M = 16781314u;

	public const uint CPU_PENTIUM_4_HT = 16781315u;

	public const uint CPU_PENTIUM_4_EE = 16781316u;

	public const uint CPU_CELERON_P4 = 16781317u;

	public const uint CPU_CELERON_D = 16781318u;

	public const uint CPU_XEON_P4 = 16781319u;

	public const uint CPU_PENTIUM_D = 16781320u;

	public const uint CPU_PENTIUM_XE = 16781321u;

	public const uint CPU_PENTIUM_M = 16785409u;

	public const uint CPU_CELERON_M = 16785410u;

	public const uint CPU_CORE_SOLO = 16793601u;

	public const uint CPU_CORE_DUO = 16793602u;

	public const uint CPU_CORE_CELERON_M = 16793603u;

	public const uint CPU_CORE_CELERON = 16793604u;

	public const uint CPU_CORE_2_DUO = 16809985u;

	public const uint CPU_CORE_2_EE = 16809986u;

	public const uint CPU_CORE_2_XEON = 16809987u;

	public const uint CPU_CORE_2_CELERON = 16809988u;

	public const uint CPU_CORE_2_QUAD = 16809989u;

	public const uint CPU_CORE_2_PENTIUM = 16809990u;

	public const uint CPU_CORE_2_CELERON_DC = 16809991u;

	public const uint CPU_CORE_2_SOLO = 16809992u;

	public const uint CPU_BONNELL_ATOM = 16842753u;

	public const uint CPU_SALTWELL_ATOM = 16843009u;

	public const uint CPU_SILVERMONT_ATOM = 16843265u;

	public const uint CPU_SILVERMONT_CELERON = 16843266u;

	public const uint CPU_SILVERMONT_PENTIUM = 16843267u;

	public const uint CPU_SILVERMONT_ATOM_X7 = 16843268u;

	public const uint CPU_SILVERMONT_ATOM_X5 = 16843269u;

	public const uint CPU_SILVERMONT_ATOM_X3 = 16843270u;

	public const uint CPU_GOLDMONT_ATOM = 16843777u;

	public const uint CPU_GOLDMONT_CELERON = 16843778u;

	public const uint CPU_GOLDMONT_PENTIUM = 16843779u;

	public const uint CPU_NEHALEM_CORE_I7 = 16908289u;

	public const uint CPU_NEHALEM_CORE_I7E = 16908290u;

	public const uint CPU_NEHALEM_XEON = 16908291u;

	public const uint CPU_NEHALEM_CORE_I3 = 16908292u;

	public const uint CPU_NEHALEM_CORE_I5 = 16908293u;

	public const uint CPU_NEHALEM_PENTIUM = 16908295u;

	public const uint CPU_NEHALEM_CELERON = 16908296u;

	public const uint CPU_SANDY_BRIDGE_CORE_I7 = 16908545u;

	public const uint CPU_SANDY_BRIDGE_CORE_I7E = 16908546u;

	public const uint CPU_SANDY_BRIDGE_XEON = 16908547u;

	public const uint CPU_SANDY_BRIDGE_CORE_I3 = 16908548u;

	public const uint CPU_SANDY_BRIDGE_CORE_I5 = 16908549u;

	public const uint CPU_SANDY_BRIDGE_PENTIUM = 16908551u;

	public const uint CPU_SANDY_BRIDGE_CELERON = 16908552u;

	public const uint CPU_HASWELL_CORE_I7 = 16908801u;

	public const uint CPU_HASWELL_CORE_I7E = 16908802u;

	public const uint CPU_HASWELL_XEON = 16908803u;

	public const uint CPU_HASWELL_CORE_I3 = 16908804u;

	public const uint CPU_HASWELL_CORE_I5 = 16908805u;

	public const uint CPU_HASWELL_PENTIUM = 16908807u;

	public const uint CPU_HASWELL_CELERON = 16908808u;

	public const uint CPU_HASWELL_CORE_M = 16908809u;

	public const uint CPU_SKYLAKE_XEON = 17039361u;

	public const uint CPU_SKYLAKE_CORE_I7 = 17039362u;

	public const uint CPU_SKYLAKE_CORE_I5 = 17039363u;

	public const uint CPU_SKYLAKE_CORE_I3 = 17039364u;

	public const uint CPU_SKYLAKE_PENTIUM = 17039365u;

	public const uint CPU_SKYLAKE_CELERON = 17039366u;

	public const uint CPU_SKYLAKE_CORE_M7 = 17039367u;

	public const uint CPU_SKYLAKE_CORE_M5 = 17039368u;

	public const uint CPU_SKYLAKE_CORE_M3 = 17039369u;

	public const uint CPU_SKYLAKE_CORE_I9EX = 17039370u;

	public const uint CPU_SKYLAKE_CORE_I9X = 17039371u;

	public const uint CPU_SKYLAKE_CORE_I7X = 17039372u;

	public const uint CPU_SKYLAKE_CORE_I5X = 17039373u;

	public const uint CPU_SKYLAKE_XEON_BRONZE = 17039374u;

	public const uint CPU_SKYLAKE_XEON_SILVER = 17039375u;

	public const uint CPU_SKYLAKE_XEON_GOLD = 17039376u;

	public const uint CPU_SKYLAKE_XEON_PLATINIUM = 17039377u;

	public const uint CPU_SKYLAKE_PENTIUM_GOLD = 17039378u;

	public const uint CPU_SKYLAKE_CORE_I9_GEN9 = 17039392u;

	public const uint CPU_SKYLAKE_CORE_I7_GEN9 = 17039393u;

	public const uint CPU_SKYLAKE_CORE_I5_GEN9 = 17039394u;

	public const uint CPU_SKYLAKE_CORE_I3_GEN9 = 17039395u;

	public const uint CPU_SKYLAKE_CORE_I9_GEN10 = 17039396u;

	public const uint CPU_SKYLAKE_CORE_I7_GEN10 = 17039397u;

	public const uint CPU_SKYLAKE_CORE_I5_GEN10 = 17039398u;

	public const uint CPU_SKYLAKE_CORE_I3_GEN10 = 17039399u;

	public const uint CPU_SKYLAKE_CORE_I9_GEN11 = 17039400u;

	public const uint CPU_SKYLAKE_CORE_I7_GEN11 = 17039401u;

	public const uint CPU_SKYLAKE_CORE_I5_GEN11 = 17039402u;

	public const uint CPU_SKYLAKE_CORE_I3_GEN11 = 17039403u;

	public const uint CPU_SKYLAKE_CORE_I9_GEN12 = 17039404u;

	public const uint CPU_SKYLAKE_CORE_I7_GEN12 = 17039405u;

	public const uint CPU_SKYLAKE_CORE_I5_GEN12 = 17039406u;

	public const uint CPU_SKYLAKE_CORE_I3_GEN12 = 17039407u;

	public const uint CPU_SKYLAKE_CELERON_GEN12 = 17039408u;

	public const uint CPU_SKYLAKE_PENTIUM_GEN12 = 17039409u;

	public const uint CPU_AMD_386 = 33554688u;

	public const uint CPU_AMD_486 = 33554944u;

	public const uint CPU_AMD_K5 = 33555456u;

	public const uint CPU_AMD_K6 = 33556480u;

	public const uint CPU_AMD_K7 = 33558528u;

	public const uint CPU_AMD_K8 = 33562624u;

	public const uint CPU_AMD_K10 = 33570816u;

	public const uint CPU_AMD_K12 = 33619968u;

	public const uint CPU_AMD_K14 = 33685504u;

	public const uint CPU_AMD_K15 = 33816576u;

	public const uint CPU_AMD_K16 = 34078720u;

	public const uint CPU_AMD_K17 = 34603008u;

	public const uint CPU_K5 = 33555457u;

	public const uint CPU_K5_GEODE = 33555458u;

	public const uint CPU_K6 = 33556481u;

	public const uint CPU_K6_2 = 33556482u;

	public const uint CPU_K6_3 = 33556483u;

	public const uint CPU_K7_ATHLON = 33558529u;

	public const uint CPU_K7_ATHLON_XP = 33558530u;

	public const uint CPU_K7_ATHLON_MP = 33558531u;

	public const uint CPU_K7_DURON = 33558532u;

	public const uint CPU_K7_SEMPRON = 33558533u;

	public const uint CPU_K7_SEMPRON_M = 33558534u;

	public const uint CPU_K8_ATHLON_64 = 33562625u;

	public const uint CPU_K8_ATHLON_64_M = 33562626u;

	public const uint CPU_K8_ATHLON_64_FX = 33562627u;

	public const uint CPU_K8_OPTERON = 33562628u;

	public const uint CPU_K8_TURION_64 = 33562629u;

	public const uint CPU_K8_SEMPRON = 33562630u;

	public const uint CPU_K8_SEMPRON_M = 33562631u;

	public const uint CPU_K8_ATHLON_64_X2 = 33562632u;

	public const uint CPU_K8_TURION_64_X2 = 33562633u;

	public const uint CPU_K8_ATHLON_NEO = 33562634u;

	public const uint CPU_K10_PHENOM = 33570817u;

	public const uint CPU_K10_PHENOM_X3 = 33570818u;

	public const uint CPU_K10_PHENOM_FX = 33570819u;

	public const uint CPU_K10_OPTERON = 33570820u;

	public const uint CPU_K10_TURION_64 = 33570821u;

	public const uint CPU_K10_TURION_64_ULTRA = 33570822u;

	public const uint CPU_K10_ATHLON_64 = 33570823u;

	public const uint CPU_K10_SEMPRON = 33570824u;

	public const uint CPU_K10_ATHLON_2 = 33570833u;

	public const uint CPU_K10_ATHLON_2_X2 = 33570827u;

	public const uint CPU_K10_ATHLON_2_X3 = 33570829u;

	public const uint CPU_K10_ATHLON_2_X4 = 33570828u;

	public const uint CPU_K10_PHENOM_II = 33570825u;

	public const uint CPU_K10_PHENOM_II_X2 = 33570826u;

	public const uint CPU_K10_PHENOM_II_X3 = 33570830u;

	public const uint CPU_K10_PHENOM_II_X4 = 33570831u;

	public const uint CPU_K10_PHENOM_II_X6 = 33570832u;

	public const uint CPU_K15_FXB = 33816577u;

	public const uint CPU_K15_OPTERON = 33816578u;

	public const uint CPU_K15_A10T = 33816579u;

	public const uint CPU_K15_A8T = 33816580u;

	public const uint CPU_K15_A6T = 33816581u;

	public const uint CPU_K15_A4T = 33816582u;

	public const uint CPU_K15_ATHLON_X4 = 33816583u;

	public const uint CPU_K15_FXV = 33816584u;

	public const uint CPU_K15_A10R = 33816585u;

	public const uint CPU_K15_A8R = 33816586u;

	public const uint CPU_K15_A6R = 33816587u;

	public const uint CPU_K15_A4R = 33816588u;

	public const uint CPU_K15_SEMPRON = 33816589u;

	public const uint CPU_K15_ATHLON_X2 = 33816590u;

	public const uint CPU_K15_FXC = 33816591u;

	public const uint CPU_K15_A10C = 33816592u;

	public const uint CPU_K15_A8C = 33816593u;

	public const uint CPU_K15_A6C = 33816594u;

	public const uint CPU_K15_A4C = 33816595u;

	public const uint CPU_K15_A12 = 33816596u;

	public const uint CPU_K15_RX = 33816597u;

	public const uint CPU_K15_GX = 33816598u;

	public const uint CPU_K15_A9 = 33816599u;

	public const uint CPU_K15_E2 = 33816600u;

	public const uint CPU_K16_A6 = 34078721u;

	public const uint CPU_K16_A4 = 34078722u;

	public const uint CPU_K16_OPTERON = 34078725u;

	public const uint CPU_K16_ATHLON = 34078726u;

	public const uint CPU_K16_SEMPRON = 34078727u;

	public const uint CPU_K16_E1 = 34078728u;

	public const uint CPU_K16_E2 = 34078729u;

	public const uint CPU_K16_A8 = 34078730u;

	public const uint CPU_K16_A10 = 34078731u;

	public const uint CPU_K16_GX = 34078732u;

	public const uint CPU_RYZEN = 34603009u;

	public const uint CPU_RYZEN_7 = 34603010u;

	public const uint CPU_RYZEN_5 = 34603011u;

	public const uint CPU_RYZEN_3 = 34603012u;

	public const uint CPU_RYZEN_TR = 34603013u;

	public const uint CPU_RYZEN_EPYC = 34603014u;

	public const uint CPU_RYZEN_M = 34603015u;

	public const uint CPU_RYZEN_7_M = 34603016u;

	public const uint CPU_RYZEN_5_M = 34603017u;

	public const uint CPU_RYZEN_3_M = 34603018u;

	public const uint CPU_RYZEN_ATHLON = 34603019u;

	public const uint CPU_RYZEN_9 = 34603020u;

	public const uint CPU_RYZEN_9_M = 34603021u;

	public const uint CPU_CX486 = 67109888u;

	public const uint CPU_CX5X86 = 67110144u;

	public const uint CPU_CX6X86 = 67110400u;

	public const uint CPU_VIA_WINCHIP = 134218752u;

	public const uint CPU_VIA_C3 = 134219776u;

	public const uint CPU_VIA_C7 = 134221824u;

	public const uint CPU_VIA_NANO = 134225920u;

	public const uint CPU_VIA_CHA = 134234112u;

	public const uint CPU_ZHAOXIN_KX = 134488064u;

	public const uint CPU_C3 = 134219777u;

	public const uint CPU_C7 = 134221825u;

	public const uint CPU_C7_M = 134221826u;

	public const uint CPU_EDEN = 134221827u;

	public const uint CPU_C7_D = 134221828u;

	public const uint CPU_NANO_X2 = 134225921u;

	public const uint CPU_EDEN_X2 = 134225922u;

	public const uint CPU_NANO_X3 = 134225923u;

	public const uint CPU_EDEN_X4 = 134225924u;

	public const uint CPU_QUADCORE = 134225925u;

	public const uint CPU_CX6X86L = 67110401u;

	public const uint CPU_MEDIAGX = 67110402u;

	public const uint CPU_CX6X86MX = 67110403u;

	public const uint CPU_MII = 67110404u;

	public const uint CPU_CRUSOE = 268435457u;

	public const uint CPU_EFFICEON = 268435458u;

	public const uint CPU_VORTEX86_SX = 536870913u;

	public const uint CPU_VORTEX86_EX = 536870914u;

	public const uint CPU_VORTEX86_DX = 536870915u;

	public const uint CPU_VORTEX86_MX = 536870916u;

	public const uint CPU_VORTEX86_DX3 = 536870917u;

	public const uint CPU_HYGON_C86 = 2214592768u;

	public const int HYBRID_CORE_TYPE_UNKNOWN = 0;

	public const int HYBRID_CORE_TYPE_QUARK = 16;

	public const int HYBRID_CORE_TYPE_ATOM = 32;

	public const int HYBRID_CORE_TYPE_KNIGHTS = 48;

	public const int HYBRID_CORE_TYPE_CORE = 64;

	public const int CACHE_TYPE_DATA = 1;

	public const int CACHE_TYPE_INSTRUCTION = 2;

	public const int CACHE_TYPE_UNIFIED = 3;

	public const int CACHE_TYPE_TRACE_CACHE = 4;

	public const int ISET_MMX = 1;

	public const int ISET_EXTENDED_MMX = 2;

	public const int ISET_3DNOW = 3;

	public const int ISET_EXTENDED_3DNOW = 4;

	public const int ISET_SSE = 5;

	public const int ISET_SSE2 = 6;

	public const int ISET_SSE3 = 7;

	public const int ISET_SSSE3 = 8;

	public const int ISET_SSE4_1 = 9;

	public const int ISET_SSE4_2 = 12;

	public const int ISET_SSE4A = 13;

	public const int ISET_XOP = 14;

	public const int ISET_X86_64 = 16;

	public const int ISET_NX = 17;

	public const int ISET_VMX = 18;

	public const int ISET_AES = 19;

	public const int ISET_AVX = 20;

	public const int ISET_AVX2 = 21;

	public const int ISET_FMA3 = 22;

	public const int ISET_FMA4 = 23;

	public const int ISET_RTM = 24;

	public const int ISET_HLE = 25;

	public const int ISET_AVX512F = 26;

	public const int ISET_SHA = 27;

	public const int HWM_CLASS_LPC = 1;

	public const int HWM_CLASS_CPU = 2;

	public const int HWM_CLASS_HDD = 4;

	public const int HWM_CLASS_DISPLAYADAPTER = 8;

	public const int HWM_CLASS_PSU = 16;

	public const int HWM_CLASS_ACPI = 32;

	public const int HWM_CLASS_RAM = 64;

	public const int HWM_CLASS_CHASSIS = 128;

	public const int HWM_CLASS_WATERCOOLER = 256;

	public const int HWM_CLASS_BATTERY = 512;

	public const int SENSOR_CLASS_VOLTAGE = 4096;

	public const int SENSOR_CLASS_TEMPERATURE = 8192;

	public const int SENSOR_CLASS_FAN = 12288;

	public const int SENSOR_CLASS_CURRENT = 16384;

	public const int SENSOR_CLASS_POWER = 20480;

	public const int SENSOR_CLASS_FAN_PWM = 24576;

	public const int SENSOR_CLASS_PUMP_PWM = 28672;

	public const int SENSOR_CLASS_WATER_LEVEL = 32768;

	public const int SENSOR_CLASS_POSITION = 36864;

	public const int SENSOR_CLASS_CAPACITY = 40960;

	public const int SENSOR_CLASS_CASEOPEN = 45056;

	public const int SENSOR_CLASS_LEVEL = 49152;

	public const int SENSOR_CLASS_COUNTER = 53248;

	public const int SENSOR_CLASS_UTILIZATION = 57344;

	public const int SENSOR_CLASS_CLOCK_SPEED = 61440;

	public const int SENSOR_CLASS_BANDWIDTH = 65536;

	public const int SENSOR_CLASS_PERF_LIMITER = 69632;

	public const int SENSOR_VOLTAGE_VCORE = 4198400;

	public const int SENSOR_VOLTAGE_3V3 = 8392704;

	public const int SENSOR_VOLTAGE_P5V = 12587008;

	public const int SENSOR_VOLTAGE_P12V = 16781312;

	public const int SENSOR_VOLTAGE_M5V = 20975616;

	public const int SENSOR_VOLTAGE_M12V = 25169920;

	public const int SENSOR_VOLTAGE_5VSB = 29364224;

	public const int SENSOR_VOLTAGE_DRAM = 33558528;

	public const int SENSOR_VOLTAGE_CPU_VTT = 37752832;

	public const int SENSOR_VOLTAGE_IOH_VCORE = 41947136;

	public const int SENSOR_VOLTAGE_IOH_PLL = 46141440;

	public const int SENSOR_VOLTAGE_CPU_PLL = 50335744;

	public const int SENSOR_VOLTAGE_PCH = 54530048;

	public const int SENSOR_VOLTAGE_CPU_VID = 58724352;

	public const int SENSOR_VOLTAGE_MAX_CPU_VID = 62918656;

	public const int SENSOR_VOLTAGE_MAX_CORESET_CPU_VID = 67112960;

	public const int SENSOR_VOLTAGE_GPU = 71307264;

	public const int SENSOR_TEMPERATURE_CPU = 4202496;

	public const int SENSOR_TEMPERATURE_VREG = 8396800;

	public const int SENSOR_TEMPERATURE_DRAM = 12591104;

	public const int SENSOR_TEMPERATURE_PCH = 16785408;

	public const int SENSOR_TEMPERATURE_GPU = 20979712;

	public const int SENSOR_TEMPERATURE_CPU_PACKAGE = 25174016;

	public const int SENSOR_TEMPERATURE_CPU_NODE = 29368320;

	public const int SENSOR_TEMPERATURE_CPU_CCD = 33562624;

	public const int SENSOR_TEMPERATURE_CPU_CORE = 37756928;

	public const int SENSOR_TEMPERATURE_MAX_CPU_CORE = 41951232;

	public const int SENSOR_TEMPERATURE_MAX_CORESET_CPU_CORE = 46145536;

	public const int SENSOR_FAN_CPU = 4206592;

	public const int SENSOR_FAN_PUMP = 8400896;

	public const int SENSOR_FAN_GPU = 25178112;

	public const int SENSOR_POWER_CPU_PACKAGE = 4214784;

	public const int SENSOR_POWER_CPU_ALL_CORES = 8409088;

	public const int SENSOR_POWER_CPU_CORE = 12603392;

	public const int SENSOR_POWER_CPU_GT = 20992000;

	public const int SENSOR_POWER_GPU = 29380608;

	public const int SENSOR_UTILIZATION_CPU_PACKAGE = 4251648;

	public const int SENSOR_UTILIZATION_CPU_CORESET = 8445952;

	public const int SENSOR_UTILIZATION_CPU_CCX = 12640256;

	public const int SENSOR_UTILIZATION_CPU_CORE = 16834560;

	public const int SENSOR_UTILIZATION_CPU_THREAD = 21028864;

	public const int SENSOR_UTILIZATION_GPU = 25223168;

	public const int SENSOR_UTILIZATION_MEMORY = 29417472;

	public const int MEMORY_TYPE_SPM_RAM = 1;

	public const int MEMORY_TYPE_RDRAM = 2;

	public const int MEMORY_TYPE_EDO_RAM = 3;

	public const int MEMORY_TYPE_FPM_RAM = 4;

	public const int MEMORY_TYPE_SDRAM = 5;

	public const int MEMORY_TYPE_DDR_SDRAM = 6;

	public const int MEMORY_TYPE_DDR2_SDRAM = 7;

	public const int MEMORY_TYPE_DDR2_SDRAM_FB = 8;

	public const int MEMORY_TYPE_DDR3_SDRAM = 9;

	public const int MEMORY_TYPE_DDR4_SDRAM = 10;

	public const int MEMORY_TYPE_DDR5_SDRAM = 11;

	public const int DISPLAY_CLOCK_DOMAIN_GRAPHICS = 0;

	public const int DISPLAY_CLOCK_DOMAIN_MEMORY = 4;

	public const int DISPLAY_CLOCK_DOMAIN_PROCESSOR = 7;

	public const int MEMORY_TYPE_SDR = 1;

	public const int MEMORY_TYPE_DDR = 2;

	public const int MEMORY_TYPE_LPDDR2 = 9;

	public const int MEMORY_TYPE_DDR2 = 3;

	public const int MEMORY_TYPE_DDR3 = 7;

	public const int MEMORY_TYPE_GDDR2 = 4;

	public const int MEMORY_TYPE_GDDR3 = 5;

	public const int MEMORY_TYPE_GDDR4 = 6;

	public const int MEMORY_TYPE_GDDR5 = 8;

	public const int MEMORY_TYPE_GDDR5X = 10;

	public const int MEMORY_TYPE_HBM1 = 11;

	public const int MEMORY_TYPE_HBM2 = 12;

	public const int MEMORY_TYPE_SDDR4 = 13;

	public const int MEMORY_TYPE_GDDR6 = 14;

	public const int MEMORY_TYPE_GDDR6X = 15;

	public const int DRIVE_FEATURE_IS_SSD = 1;

	public const int DRIVE_FEATURE_SMART = 2;

	public const int DRIVE_FEATURE_TRIM = 4;

	public const int DRIVE_FEATURE_IS_REMOVABLE = 16;

	public const int BUS_TYPE_SCSI = 1;

	public const int BUS_TYPE_ATAPI = 2;

	public const int BUS_TYPE_ATA = 3;

	public const int BUS_TYPE_IEEE1394 = 4;

	public const int BUS_TYPE_SSA = 5;

	public const int BUS_TYPE_FIBRE = 6;

	public const int BUS_TYPE_USB = 7;

	public const int BUS_TYPE_RAID = 8;

	public const int BUS_TYPE_ISCSI = 9;

	public const int BUS_TYPE_SAS = 10;

	public const int BUS_TYPE_SATA = 11;

	public const int BUS_TYPE_SD = 12;

	public const int BUS_TYPE_MMC = 13;

	public const int BUS_TYPE_VIRTUAL = 14;

	public const int BUS_TYPE_FILEBACKEDVIRTUAL = 15;

	public const int BUS_TYPE_SPACES = 16;

	public const int BUS_TYPE_NVME = 17;

	public const string szDllPath = "";

	public const string szDllFilename = "cpuidsdk.dll";

	protected const string szDllName = "cpuidsdk.dll";

	protected IntPtr objptr = IntPtr.Zero;

	public bool IS_F_DEFINED(float _f)
	{
		return _f > 0f;
	}

	public bool IS_F_DEFINED(double _f)
	{
		return _f > 0.0;
	}

	public bool IS_I_DEFINED(int _i)
	{
		return (_i != I_UNDEFINED_VALUE) ? true : false;
	}

	public bool IS_I_DEFINED(uint _i)
	{
		return (_i != (uint)I_UNDEFINED_VALUE) ? true : false;
	}

	public bool IS_I_DEFINED(short _i)
	{
		return (_i != (short)I_UNDEFINED_VALUE) ? true : false;
	}

	public bool IS_I_DEFINED(ushort _i)
	{
		return (_i != (ushort)I_UNDEFINED_VALUE) ? true : false;
	}

	public bool IS_I_DEFINED(sbyte _i)
	{
		return (_i != (sbyte)I_UNDEFINED_VALUE) ? true : false;
	}

	public bool IS_I_DEFINED(byte _i)
	{
		return (_i != (byte)I_UNDEFINED_VALUE) ? true : false;
	}

	[DllImport("cpuidsdk.dll", EntryPoint = "QueryInterface")]
	protected static extern IntPtr CPUIDSDK_fp_QueryInterface(uint _code);

	public bool CreateInstance()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(834560892u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_CreateInstance cPUIDSDK_fp_CreateInstance = (CPUIDSDK_fp_CreateInstance)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_CreateInstance));
				objptr = cPUIDSDK_fp_CreateInstance();
				if (objptr != IntPtr.Zero)
				{
					return true;
				}
				return false;
			}
			objptr = IntPtr.Zero;
			return false;
		}
		catch (Exception ex)
		{
			string message = ex.Message;
			return false;
		}
	}

	public void DestroyInstance()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3385693083u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_DestroyInstance cPUIDSDK_fp_DestroyInstance = (CPUIDSDK_fp_DestroyInstance)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_DestroyInstance));
				cPUIDSDK_fp_DestroyInstance(objptr);
			}
		}
		catch
		{
		}
	}

	public bool Init(string _szDllPath, string _szDllFilename, uint _config_flag, ref int _errorcode, ref int _extended_errorcode)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1663485517u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_Init cPUIDSDK_fp_Init = (CPUIDSDK_fp_Init)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_Init));
				int num = cPUIDSDK_fp_Init(objptr, _szDllPath, _szDllFilename, (int)_config_flag, ref _errorcode, ref _extended_errorcode);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			_errorcode = 16;
			return false;
		}
		catch
		{
			return false;
		}
	}

	public void Close()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3286337475u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_Close cPUIDSDK_fp_Close = (CPUIDSDK_fp_Close)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_Close));
				cPUIDSDK_fp_Close(objptr);
			}
		}
		catch
		{
		}
	}

	public void RefreshInformation()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1183878433u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_RefreshInformation cPUIDSDK_fp_RefreshInformation = (CPUIDSDK_fp_RefreshInformation)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_RefreshInformation));
				cPUIDSDK_fp_RefreshInformation(objptr);
			}
		}
		catch
		{
		}
	}

	public void GetDllVersion(ref int _version)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1993797037u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDllVersion cPUIDSDK_fp_GetDllVersion = (CPUIDSDK_fp_GetDllVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDllVersion));
				cPUIDSDK_fp_GetDllVersion(objptr, ref _version);
			}
		}
		catch
		{
		}
	}

	public int GetNumberOfProcessors()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3681662691u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNbProcessors cPUIDSDK_fp_GetNbProcessors = (CPUIDSDK_fp_GetNbProcessors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNbProcessors));
				return cPUIDSDK_fp_GetNbProcessors(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorFamily(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3859532823u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorFamily cPUIDSDK_fp_GetProcessorFamily = (CPUIDSDK_fp_GetProcessorFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorFamily));
				return cPUIDSDK_fp_GetProcessorFamily(objptr, _proc_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorCoreSetCount(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1565702821u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreSetCount cPUIDSDK_fp_GetProcessorCoreSetCount = (CPUIDSDK_fp_GetProcessorCoreSetCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreSetCount));
				return cPUIDSDK_fp_GetProcessorCoreSetCount(objptr, _proc_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorCoreCount(int _proc_index, int _core_set)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2060907949u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreCount cPUIDSDK_fp_GetProcessorCoreCount = (CPUIDSDK_fp_GetProcessorCoreCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreCount));
				return cPUIDSDK_fp_GetProcessorCoreCount(objptr, _proc_index, _core_set);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorThreadCount(int _proc_index, int _core_set)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1875828637u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorThreadCount cPUIDSDK_fp_GetProcessorThreadCount = (CPUIDSDK_fp_GetProcessorThreadCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorThreadCount));
				return cPUIDSDK_fp_GetProcessorThreadCount(objptr, _proc_index, _core_set);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorCoreSetType(int _proc_index, int _core_set)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3105190442u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreSetType cPUIDSDK_fp_GetProcessorCoreSetType = (CPUIDSDK_fp_GetProcessorCoreSetType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreSetType));
				return cPUIDSDK_fp_GetProcessorCoreSetType(objptr, _proc_index, _core_set);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorCoreThreadCount(int _proc_index, int _core_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1583529157u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreThreadCount cPUIDSDK_fp_GetProcessorCoreThreadCount = (CPUIDSDK_fp_GetProcessorCoreThreadCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreThreadCount));
				return cPUIDSDK_fp_GetProcessorCoreThreadCount(objptr, _proc_index, _core_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1594932769u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorThreadAPICID cPUIDSDK_fp_GetProcessorThreadAPICID = (CPUIDSDK_fp_GetProcessorThreadAPICID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorThreadAPICID));
				return cPUIDSDK_fp_GetProcessorThreadAPICID(objptr, _proc_index, _core_index, _thread_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetProcessorName(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1597161061u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorName cPUIDSDK_fp_GetProcessorName = (CPUIDSDK_fp_GetProcessorName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorName));
				IntPtr ptr = cPUIDSDK_fp_GetProcessorName(objptr, _proc_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetProcessorCodeName(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3225387135u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCodeName cPUIDSDK_fp_GetProcessorCodeName = (CPUIDSDK_fp_GetProcessorCodeName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCodeName));
				IntPtr ptr = cPUIDSDK_fp_GetProcessorCodeName(objptr, _proc_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetProcessorSpecification(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(338045004u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorSpecification cPUIDSDK_fp_GetProcessorSpecification = (CPUIDSDK_fp_GetProcessorSpecification)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorSpecification));
				IntPtr ptr = cPUIDSDK_fp_GetProcessorSpecification(objptr, _proc_index);
				return Marshal.PtrToStringAnsi(ptr);
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetProcessorPackage(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1053719964u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorPackage cPUIDSDK_fp_GetProcessorPackage = (CPUIDSDK_fp_GetProcessorPackage)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorPackage));
				IntPtr ptr = cPUIDSDK_fp_GetProcessorPackage(objptr, _proc_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetProcessorStepping(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1122665941u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorStepping cPUIDSDK_fp_GetProcessorStepping = (CPUIDSDK_fp_GetProcessorStepping)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStepping));
				IntPtr ptr = cPUIDSDK_fp_GetProcessorStepping(objptr, _proc_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public float GetProcessorTDP(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3714431691u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorTDP cPUIDSDK_fp_GetProcessorTDP = (CPUIDSDK_fp_GetProcessorTDP)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorTDP));
				return cPUIDSDK_fp_GetProcessorTDP(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorManufacturingProcess(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3159193754u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorManufacturingProcess cPUIDSDK_fp_GetProcessorManufacturingProcess = (CPUIDSDK_fp_GetProcessorManufacturingProcess)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorManufacturingProcess));
				return cPUIDSDK_fp_GetProcessorManufacturingProcess(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public bool IsProcessorInstructionSetAvailable(int _proc_index, int _iset)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(480262464u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_IsProcessorInstructionSetAvailable cPUIDSDK_fp_IsProcessorInstructionSetAvailable = (CPUIDSDK_fp_IsProcessorInstructionSetAvailable)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_IsProcessorInstructionSetAvailable));
				int num = cPUIDSDK_fp_IsProcessorInstructionSetAvailable(objptr, _proc_index, _iset);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public float GetProcessorCoreClockFrequency(int _proc_index, int _core_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(266346432u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreClockFrequency cPUIDSDK_fp_GetProcessorCoreClockFrequency = (CPUIDSDK_fp_GetProcessorCoreClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreClockFrequency));
				return cPUIDSDK_fp_GetProcessorCoreClockFrequency(objptr, _proc_index, _core_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorCoreClockMultiplier(int _proc_index, int _core_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3484917615u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreClockMultiplier cPUIDSDK_fp_GetProcessorCoreClockMultiplier = (CPUIDSDK_fp_GetProcessorCoreClockMultiplier)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreClockMultiplier));
				return cPUIDSDK_fp_GetProcessorCoreClockMultiplier(objptr, _proc_index, _core_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorCoreTemperature(int _proc_index, int _core_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2575119098u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCoreTemperature cPUIDSDK_fp_GetProcessorCoreTemperature = (CPUIDSDK_fp_GetProcessorCoreTemperature)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreTemperature));
				return cPUIDSDK_fp_GetProcessorCoreTemperature(objptr, _proc_index, _core_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetBusFrequency()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1097106121u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetBusFrequency cPUIDSDK_fp_GetBusFrequency = (CPUIDSDK_fp_GetBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBusFrequency));
				return cPUIDSDK_fp_GetBusFrequency(objptr);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorRatedBusFrequency(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1512485965u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorRatedBusFrequency cPUIDSDK_fp_GetProcessorRatedBusFrequency = (CPUIDSDK_fp_GetProcessorRatedBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorRatedBusFrequency));
				return cPUIDSDK_fp_GetProcessorRatedBusFrequency(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorStockClockFrequency(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1194102361u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorStockClockFrequency cPUIDSDK_fp_GetProcessorStockClockFrequency = (CPUIDSDK_fp_GetProcessorStockClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStockClockFrequency));
				return cPUIDSDK_fp_GetProcessorStockClockFrequency(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetProcessorStockBusFrequency(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(122687136u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorStockBusFrequency cPUIDSDK_fp_GetProcessorStockBusFrequency = (CPUIDSDK_fp_GetProcessorStockBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStockBusFrequency));
				return cPUIDSDK_fp_GetProcessorStockBusFrequency(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorMaxCacheLevel(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(354167352u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorMaxCacheLevel cPUIDSDK_fp_GetProcessorMaxCacheLevel = (CPUIDSDK_fp_GetProcessorMaxCacheLevel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorMaxCacheLevel));
				return cPUIDSDK_fp_GetProcessorMaxCacheLevel(objptr, _proc_index);
			}
			return 0;
		}
		catch
		{
			return 0;
		}
	}

	public void GetProcessorCacheParameters(int _proc_index, int _core_set, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3749166831u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorCacheParameters cPUIDSDK_fp_GetProcessorCacheParameters = (CPUIDSDK_fp_GetProcessorCacheParameters)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCacheParameters));
				cPUIDSDK_fp_GetProcessorCacheParameters(objptr, _proc_index, _core_set, _cache_level, _cache_type, ref _NbCaches, ref _size);
			}
		}
		catch
		{
		}
	}

	public uint GetProcessorID(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4048544415u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorID cPUIDSDK_fp_GetProcessorID = (CPUIDSDK_fp_GetProcessorID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorID));
				return (uint)cPUIDSDK_fp_GetProcessorID(objptr, _proc_index);
			}
			return 0u;
		}
		catch
		{
			return 0u;
		}
	}

	public float GetProcessorVoltageID(int _proc_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1449307333u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorVoltageID cPUIDSDK_fp_GetProcessorVoltageID = (CPUIDSDK_fp_GetProcessorVoltageID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorVoltageID));
				return cPUIDSDK_fp_GetProcessorVoltageID(objptr, _proc_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryType()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3845245539u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryType cPUIDSDK_fp_GetMemoryType = (CPUIDSDK_fp_GetMemoryType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryType));
				return cPUIDSDK_fp_GetMemoryType(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemorySize()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3823355847u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemorySize cPUIDSDK_fp_GetMemorySize = (CPUIDSDK_fp_GetMemorySize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemorySize));
				return cPUIDSDK_fp_GetMemorySize(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public float GetMemoryClockFrequency()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(644500692u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryClockFrequency cPUIDSDK_fp_GetMemoryClockFrequency = (CPUIDSDK_fp_GetMemoryClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryClockFrequency));
				return cPUIDSDK_fp_GetMemoryClockFrequency(objptr);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryNumberOfChannels()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1189252549u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryNumberOfChannels cPUIDSDK_fp_GetMemoryNumberOfChannels = (CPUIDSDK_fp_GetMemoryNumberOfChannels)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryNumberOfChannels));
				return cPUIDSDK_fp_GetMemoryNumberOfChannels(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public float GetMemoryCASLatency()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3711023715u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryCASLatency cPUIDSDK_fp_GetMemoryCASLatency = (CPUIDSDK_fp_GetMemoryCASLatency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryCASLatency));
				return cPUIDSDK_fp_GetMemoryCASLatency(objptr);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryRAStoCASDelay()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3550193463u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryRAStoCASDelay cPUIDSDK_fp_GetMemoryRAStoCASDelay = (CPUIDSDK_fp_GetMemoryRAStoCASDelay)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryRAStoCASDelay));
				return cPUIDSDK_fp_GetMemoryRAStoCASDelay(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryRASPrecharge()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(551567808u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryRASPrecharge cPUIDSDK_fp_GetMemoryRASPrecharge = (CPUIDSDK_fp_GetMemoryRASPrecharge)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryRASPrecharge));
				return cPUIDSDK_fp_GetMemoryRASPrecharge(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryTRAS()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1779094549u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryTRAS cPUIDSDK_fp_GetMemoryTRAS = (CPUIDSDK_fp_GetMemoryTRAS)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryTRAS));
				return cPUIDSDK_fp_GetMemoryTRAS(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryTRC()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3232203087u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryTRC cPUIDSDK_fp_GetMemoryTRC = (CPUIDSDK_fp_GetMemoryTRC)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryTRC));
				return cPUIDSDK_fp_GetMemoryTRC(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryCommandRate()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3480723183u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryCommandRate cPUIDSDK_fp_GetMemoryCommandRate = (CPUIDSDK_fp_GetMemoryCommandRate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryCommandRate));
				return cPUIDSDK_fp_GetMemoryCommandRate(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetNorthBridgeVendor()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(998012664u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNorthBridgeVendor cPUIDSDK_fp_GetNorthBridgeVendor = (CPUIDSDK_fp_GetNorthBridgeVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeVendor));
				IntPtr ptr = cPUIDSDK_fp_GetNorthBridgeVendor(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetNorthBridgeModel()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3951941403u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNorthBridgeModel cPUIDSDK_fp_GetNorthBridgeModel = (CPUIDSDK_fp_GetNorthBridgeModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeModel));
				IntPtr ptr = cPUIDSDK_fp_GetNorthBridgeModel(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetNorthBridgeRevision()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1686948121u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNorthBridgeRevision cPUIDSDK_fp_GetNorthBridgeRevision = (CPUIDSDK_fp_GetNorthBridgeRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeRevision));
				IntPtr ptr = cPUIDSDK_fp_GetNorthBridgeRevision(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSouthBridgeVendor()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1952770249u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSouthBridgeVendor cPUIDSDK_fp_GetSouthBridgeVendor = (CPUIDSDK_fp_GetSouthBridgeVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeVendor));
				IntPtr ptr = cPUIDSDK_fp_GetSouthBridgeVendor(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSouthBridgeModel()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(250748388u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSouthBridgeModel cPUIDSDK_fp_GetSouthBridgeModel = (CPUIDSDK_fp_GetSouthBridgeModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeModel));
				IntPtr ptr = cPUIDSDK_fp_GetSouthBridgeModel(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSouthBridgeRevision()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1282840813u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSouthBridgeRevision cPUIDSDK_fp_GetSouthBridgeRevision = (CPUIDSDK_fp_GetSouthBridgeRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeRevision));
				IntPtr ptr = cPUIDSDK_fp_GetSouthBridgeRevision(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public void GetMemorySlotsConfig(ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1877270473u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemorySlotsConfig cPUIDSDK_fp_GetMemorySlotsConfig = (CPUIDSDK_fp_GetMemorySlotsConfig)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemorySlotsConfig));
				cPUIDSDK_fp_GetMemorySlotsConfig(objptr, ref _nbslots, ref _nbusedslots, ref _slotmap_h, ref _slotmap_l, ref _maxmodulesize);
			}
		}
		catch
		{
		}
	}

	public string GetBIOSVendor()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(767318904u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetBIOSVendor cPUIDSDK_fp_GetBIOSVendor = (CPUIDSDK_fp_GetBIOSVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSVendor));
				IntPtr ptr = cPUIDSDK_fp_GetBIOSVendor(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetBIOSVersion()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1490203045u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetBIOSVersion cPUIDSDK_fp_GetBIOSVersion = (CPUIDSDK_fp_GetBIOSVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSVersion));
				IntPtr ptr = cPUIDSDK_fp_GetBIOSVersion(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetBIOSDate()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1411164217u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetBIOSDate cPUIDSDK_fp_GetBIOSDate = (CPUIDSDK_fp_GetBIOSDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSDate));
				IntPtr ptr = cPUIDSDK_fp_GetBIOSDate(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMainboardVendor()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1755500869u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMainboardVendor cPUIDSDK_fp_GetMainboardVendor = (CPUIDSDK_fp_GetMainboardVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardVendor));
				IntPtr ptr = cPUIDSDK_fp_GetMainboardVendor(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMainboardModel()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2727298334u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMainboardModel cPUIDSDK_fp_GetMainboardModel = (CPUIDSDK_fp_GetMainboardModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardModel));
				IntPtr ptr = cPUIDSDK_fp_GetMainboardModel(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMainboardRevision()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4232312967u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMainboardRevision cPUIDSDK_fp_GetMainboardRevision = (CPUIDSDK_fp_GetMainboardRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardRevision));
				IntPtr ptr = cPUIDSDK_fp_GetMainboardRevision(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMainboardSerialNumber()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(276439284u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMainboardSerialNumber cPUIDSDK_fp_GetMainboardSerialNumber = (CPUIDSDK_fp_GetMainboardSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardSerialNumber));
				IntPtr ptr = cPUIDSDK_fp_GetMainboardSerialNumber(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemManufacturer()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(350366148u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemManufacturer cPUIDSDK_fp_GetSystemManufacturer = (CPUIDSDK_fp_GetSystemManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemManufacturer));
				IntPtr ptr = cPUIDSDK_fp_GetSystemManufacturer(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemProductName()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1363583629u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemProductName cPUIDSDK_fp_GetSystemProductName = (CPUIDSDK_fp_GetSystemProductName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemProductName));
				IntPtr ptr = cPUIDSDK_fp_GetSystemProductName(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemVersion()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(700076916u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemVersion cPUIDSDK_fp_GetSystemVersion = (CPUIDSDK_fp_GetSystemVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemVersion));
				IntPtr ptr = cPUIDSDK_fp_GetSystemVersion(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemSerialNumber()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3612061335u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemSerialNumber cPUIDSDK_fp_GetSystemSerialNumber = (CPUIDSDK_fp_GetSystemSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemSerialNumber));
				IntPtr ptr = cPUIDSDK_fp_GetSystemSerialNumber(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemUUID()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2827440398u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemUUID cPUIDSDK_fp_GetSystemUUID = (CPUIDSDK_fp_GetSystemUUID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemUUID));
				IntPtr ptr = cPUIDSDK_fp_GetSystemUUID(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetChassisManufacturer()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2276659046u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetChassisManufacturer cPUIDSDK_fp_GetChassisManufacturer = (CPUIDSDK_fp_GetChassisManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisManufacturer));
				IntPtr ptr = cPUIDSDK_fp_GetChassisManufacturer(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetChassisType()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1789187401u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetChassisType cPUIDSDK_fp_GetChassisType = (CPUIDSDK_fp_GetChassisType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisType));
				IntPtr ptr = cPUIDSDK_fp_GetChassisType(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetChassisSerialNumber()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1595194921u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetChassisSerialNumber cPUIDSDK_fp_GetChassisSerialNumber = (CPUIDSDK_fp_GetChassisSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisSerialNumber));
				IntPtr ptr = cPUIDSDK_fp_GetChassisSerialNumber(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public bool GetMemoryInfosExt(ref string _szLocation, ref string _szUsage, ref string _szCorrection)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1509209065u);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr _szLocation2 = IntPtr.Zero;
				IntPtr _szUsage2 = IntPtr.Zero;
				IntPtr _szCorrection2 = IntPtr.Zero;
				CPUIDSDK_fp_GetMemoryInfosExt cPUIDSDK_fp_GetMemoryInfosExt = (CPUIDSDK_fp_GetMemoryInfosExt)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryInfosExt));
				int num = cPUIDSDK_fp_GetMemoryInfosExt(objptr, ref _szLocation2, ref _szUsage2, ref _szCorrection2);
				if (num == 1)
				{
					_szLocation = Marshal.PtrToStringAnsi(_szLocation2);
					Marshal.FreeBSTR(_szLocation2);
					_szUsage = Marshal.PtrToStringAnsi(_szUsage2);
					Marshal.FreeBSTR(_szUsage2);
					_szCorrection = Marshal.PtrToStringAnsi(_szCorrection2);
					Marshal.FreeBSTR(_szCorrection2);
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public int GetNumberOfMemoryDevices()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1290443221u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfMemoryDevices cPUIDSDK_fp_GetNumberOfMemoryDevices = (CPUIDSDK_fp_GetNumberOfMemoryDevices)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfMemoryDevices));
				return cPUIDSDK_fp_GetNumberOfMemoryDevices(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public bool GetMemoryDeviceInfos(int _device_index, ref int _size, ref string _szFormat)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2281902086u);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr _szFormat2 = IntPtr.Zero;
				CPUIDSDK_fp_GetMemoryDeviceInfos cPUIDSDK_fp_GetMemoryDeviceInfos = (CPUIDSDK_fp_GetMemoryDeviceInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfos));
				int num = cPUIDSDK_fp_GetMemoryDeviceInfos(objptr, _device_index, ref _size, ref _szFormat2);
				if (num == 1)
				{
					_szFormat = Marshal.PtrToStringAnsi(_szFormat2);
					Marshal.FreeBSTR(_szFormat2);
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public bool GetMemoryDeviceInfosExt(int _device_index, ref string _szDesignation, ref string _szType, ref int _total_width, ref int _data_width, ref int _speed)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3813787299u);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr _szDesignation2 = IntPtr.Zero;
				IntPtr _szType2 = IntPtr.Zero;
				CPUIDSDK_fp_GetMemoryDeviceInfosExt cPUIDSDK_fp_GetMemoryDeviceInfosExt = (CPUIDSDK_fp_GetMemoryDeviceInfosExt)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfosExt));
				int num = cPUIDSDK_fp_GetMemoryDeviceInfosExt(objptr, _device_index, ref _szDesignation2, ref _szType2, ref _total_width, ref _data_width, ref _speed);
				if (num == 1)
				{
					_szDesignation = Marshal.PtrToStringAnsi(_szDesignation2);
					Marshal.FreeBSTR(_szDesignation2);
					_szType = Marshal.PtrToStringAnsi(_szType2);
					Marshal.FreeBSTR(_szType2);
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public int GetMemoryMaxCapacity()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4030324851u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryMaxCapacity cPUIDSDK_fp_GetMemoryMaxCapacity = (CPUIDSDK_fp_GetMemoryMaxCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryMaxCapacity));
				return cPUIDSDK_fp_GetMemoryMaxCapacity(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetMemoryMaxNumberOfDevices()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(631262016u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMemoryMaxNumberOfDevices cPUIDSDK_fp_GetMemoryMaxNumberOfDevices = (CPUIDSDK_fp_GetMemoryMaxNumberOfDevices)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryMaxNumberOfDevices));
				return cPUIDSDK_fp_GetMemoryMaxNumberOfDevices(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetProcessorSockets()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3312290523u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetProcessorSockets cPUIDSDK_fp_GetProcessorSockets = (CPUIDSDK_fp_GetProcessorSockets)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorSockets));
				return cPUIDSDK_fp_GetProcessorSockets(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetSystemSKU()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3731864799u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemSKU cPUIDSDK_fp_GetSystemSKU = (CPUIDSDK_fp_GetSystemSKU)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemSKU));
				IntPtr ptr = cPUIDSDK_fp_GetSystemSKU(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSystemFamily()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1656014185u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSystemFamily cPUIDSDK_fp_GetSystemFamily = (CPUIDSDK_fp_GetSystemFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemFamily));
				IntPtr ptr = cPUIDSDK_fp_GetSystemFamily(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetNumberOfSPDModules()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1384686865u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfSPDModules cPUIDSDK_fp_GetNumberOfSPDModules = (CPUIDSDK_fp_GetNumberOfSPDModules)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfSPDModules));
				return cPUIDSDK_fp_GetNumberOfSPDModules(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleType(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2244545426u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleType cPUIDSDK_fp_GetSPDModuleType = (CPUIDSDK_fp_GetSPDModuleType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleType));
				return cPUIDSDK_fp_GetSPDModuleType(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleSize(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1083605293u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleSize cPUIDSDK_fp_GetSPDModuleSize = (CPUIDSDK_fp_GetSPDModuleSize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSize));
				return cPUIDSDK_fp_GetSPDModuleSize(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetSPDModuleFormat(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1336319821u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleFormat cPUIDSDK_fp_GetSPDModuleFormat = (CPUIDSDK_fp_GetSPDModuleFormat)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleFormat));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModuleFormat(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSPDModuleManufacturer(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(211556664u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleManufacturer cPUIDSDK_fp_GetSPDModuleManufacturer = (CPUIDSDK_fp_GetSPDModuleManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturer));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModuleManufacturer(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public bool GetSPDModuleManufacturerID(int _spd_index, byte[] _id)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1777652713u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleManufacturerID cPUIDSDK_fp_GetSPDModuleManufacturerID = (CPUIDSDK_fp_GetSPDModuleManufacturerID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturerID));
				int num = cPUIDSDK_fp_GetSPDModuleManufacturerID(objptr, _spd_index, _id);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public string GetSPDModuleDRAMManufacturer(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(981759240u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleDRAMManufacturer cPUIDSDK_fp_GetSPDModuleDRAMManufacturer = (CPUIDSDK_fp_GetSPDModuleDRAMManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleDRAMManufacturer));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModuleDRAMManufacturer(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetSPDModuleMaxFrequency(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(546193692u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleMaxFrequency cPUIDSDK_fp_GetSPDModuleMaxFrequency = (CPUIDSDK_fp_GetSPDModuleMaxFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMaxFrequency));
				return cPUIDSDK_fp_GetSPDModuleMaxFrequency(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetSPDModuleSpecification(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1347592357u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleSpecification cPUIDSDK_fp_GetSPDModuleSpecification = (CPUIDSDK_fp_GetSPDModuleSpecification)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSpecification));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModuleSpecification(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSPDModulePartNumber(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1813174309u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModulePartNumber cPUIDSDK_fp_GetSPDModulePartNumber = (CPUIDSDK_fp_GetSPDModulePartNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModulePartNumber));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModulePartNumber(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetSPDModuleSerialNumber(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3391067199u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleSerialNumber cPUIDSDK_fp_GetSPDModuleSerialNumber = (CPUIDSDK_fp_GetSPDModuleSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSerialNumber));
				IntPtr ptr = cPUIDSDK_fp_GetSPDModuleSerialNumber(objptr, _spd_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public float GetSPDModuleMinTRCD(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3454901211u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleMinTRCD cPUIDSDK_fp_GetSPDModuleMinTRCD = (CPUIDSDK_fp_GetSPDModuleMinTRCD)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRCD));
				return cPUIDSDK_fp_GetSPDModuleMinTRCD(objptr, _spd_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetSPDModuleMinTRP(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2314408934u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleMinTRP cPUIDSDK_fp_GetSPDModuleMinTRP = (CPUIDSDK_fp_GetSPDModuleMinTRP)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRP));
				return cPUIDSDK_fp_GetSPDModuleMinTRP(objptr, _spd_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetSPDModuleMinTRAS(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2620995698u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleMinTRAS cPUIDSDK_fp_GetSPDModuleMinTRAS = (CPUIDSDK_fp_GetSPDModuleMinTRAS)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRAS));
				return cPUIDSDK_fp_GetSPDModuleMinTRAS(objptr, _spd_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetSPDModuleMinTRC(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2203125410u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleMinTRC cPUIDSDK_fp_GetSPDModuleMinTRC = (CPUIDSDK_fp_GetSPDModuleMinTRC)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRC));
				return cPUIDSDK_fp_GetSPDModuleMinTRC(objptr, _spd_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleManufacturingDate(int _spd_index, ref int _year, ref int _week)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1901650609u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleManufacturingDate cPUIDSDK_fp_GetSPDModuleManufacturingDate = (CPUIDSDK_fp_GetSPDModuleManufacturingDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturingDate));
				return cPUIDSDK_fp_GetSPDModuleManufacturingDate(objptr, _spd_index, ref _year, ref _week);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleNumberOfBanks(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2056189213u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleNumberOfBanks cPUIDSDK_fp_GetSPDModuleNumberOfBanks = (CPUIDSDK_fp_GetSPDModuleNumberOfBanks)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfBanks));
				return cPUIDSDK_fp_GetSPDModuleNumberOfBanks(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleDataWidth(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3736190307u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleDataWidth cPUIDSDK_fp_GetSPDModuleDataWidth = (CPUIDSDK_fp_GetSPDModuleDataWidth)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleDataWidth));
				return cPUIDSDK_fp_GetSPDModuleDataWidth(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public float GetSPDModuleTemperature(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1708182433u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleTemperature cPUIDSDK_fp_GetSPDModuleTemperature = (CPUIDSDK_fp_GetSPDModuleTemperature)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleTemperature));
				return cPUIDSDK_fp_GetSPDModuleTemperature(objptr, _spd_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleNumberOfProfiles(int _spd_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1327930957u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleNumberOfProfiles cPUIDSDK_fp_GetSPDModuleNumberOfProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfProfiles));
				return cPUIDSDK_fp_GetSPDModuleNumberOfProfiles(objptr, _spd_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public void GetSPDModuleProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3738156447u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleProfileInfos cPUIDSDK_fp_GetSPDModuleProfileInfos = (CPUIDSDK_fp_GetSPDModuleProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleProfileInfos));
				cPUIDSDK_fp_GetSPDModuleProfileInfos(objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _nominal_vdd);
			}
		}
		catch
		{
		}
	}

	public int GetSPDModuleNumberOfEPPProfiles(int _spd_index, ref int _epp_revision)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2089613593u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles cPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles));
				return cPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles(objptr, _spd_index, ref _epp_revision);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public void GetSPDModuleEPPProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1545517117u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleEPPProfileInfos cPUIDSDK_fp_GetSPDModuleEPPProfileInfos = (CPUIDSDK_fp_GetSPDModuleEPPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleEPPProfileInfos));
				cPUIDSDK_fp_GetSPDModuleEPPProfileInfos(objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd);
			}
		}
		catch
		{
		}
	}

	public int GetSPDModuleNumberOfXMPProfiles(int _spd_index, ref int _xmp_revision)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(782130492u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles cPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles));
				return cPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles(objptr, _spd_index, ref _xmp_revision);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetSPDModuleXMPProfileNumberOfCL(int _spd_index, int _profile_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1609351129u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL cPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL = (CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL));
				return cPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL(objptr, _spd_index, _profile_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public void GetSPDModuleXMPProfileCLInfos(int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(767581056u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos cPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos = (CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos));
				cPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos(objptr, _spd_index, _profile_index, _cl_index, ref _frequency, ref _CL);
			}
		}
		catch
		{
		}
	}

	public void GetSPDModuleXMPProfileInfos(int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(240000156u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleXMPProfileInfos cPUIDSDK_fp_GetSPDModuleXMPProfileInfos = (CPUIDSDK_fp_GetSPDModuleXMPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileInfos));
				cPUIDSDK_fp_GetSPDModuleXMPProfileInfos(objptr, _spd_index, _profile_index, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd, ref _max_freq, ref _max_CL);
			}
		}
		catch
		{
		}
	}

	public int GetSPDModuleNumberOfAMPProfiles(int _spd_index, ref int _amp_revision)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3492913251u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles cPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles));
				return cPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles(objptr, _spd_index, ref _amp_revision);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public void GetSPDModuleAMPProfileInfos(int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1876615093u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleAMPProfileInfos cPUIDSDK_fp_GetSPDModuleAMPProfileInfos = (CPUIDSDK_fp_GetSPDModuleAMPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleAMPProfileInfos));
				cPUIDSDK_fp_GetSPDModuleAMPProfileInfos(objptr, _spd_index, _profile_index, ref _frequency, ref _min_cycle_time, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC);
			}
		}
		catch
		{
		}
	}

	public int GetSPDModuleRawData(int _spd_index, int _offset)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3833579775u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSPDModuleRawData cPUIDSDK_fp_GetSPDModuleRawData = (CPUIDSDK_fp_GetSPDModuleRawData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleRawData));
				return cPUIDSDK_fp_GetSPDModuleRawData(objptr, _spd_index, _offset);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetNumberOfDisplayAdapter()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1388881297u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfDisplayAdapter cPUIDSDK_fp_GetNumberOfDisplayAdapter = (CPUIDSDK_fp_GetNumberOfDisplayAdapter)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfDisplayAdapter));
				return cPUIDSDK_fp_GetNumberOfDisplayAdapter(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetDisplayAdapterName(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2389384406u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterName cPUIDSDK_fp_GetDisplayAdapterName = (CPUIDSDK_fp_GetDisplayAdapterName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterName));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterName(objptr, _adapter_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetDisplayAdapterCodeName(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(27132732u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterCodeName cPUIDSDK_fp_GetDisplayAdapterCodeName = (CPUIDSDK_fp_GetDisplayAdapterCodeName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCodeName));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterCodeName(objptr, _adapter_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetDisplayAdapterNumberOfPerformanceLevels(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3365769531u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels cPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels = (CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels));
				return cPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels(objptr, _adapter_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetDisplayAdapterCurrentPerformanceLevel(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1334353681u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel cPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel = (CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel));
				return cPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel(objptr, _adapter_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetDisplayAdapterPerformanceLevelName(int _adapter_index, int _perf_level)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2952224750u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName cPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName = (CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName(objptr, _adapter_index, _perf_level);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public float GetDisplayAdapterClock(int _adapter_index, int _perf_level, int _domain)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3356463135u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterClock cPUIDSDK_fp_GetDisplayAdapterClock = (CPUIDSDK_fp_GetDisplayAdapterClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterClock));
				return cPUIDSDK_fp_GetDisplayAdapterClock(objptr, _adapter_index, _perf_level, _domain);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetDisplayAdapterStockClock(int _adapter_index, int _perf_level, int _domain)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1314954433u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterStockClock cPUIDSDK_fp_GetDisplayAdapterStockClock = (CPUIDSDK_fp_GetDisplayAdapterStockClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterStockClock));
				return cPUIDSDK_fp_GetDisplayAdapterStockClock(objptr, _adapter_index, _perf_level, _domain);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetDisplayAdapterManufacturingProcess(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2133917281u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess cPUIDSDK_fp_GetDisplayAdapterManufacturingProcess = (CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess));
				return cPUIDSDK_fp_GetDisplayAdapterManufacturingProcess(objptr, _adapter_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetDisplayAdapterTemperature(int _adapter_index, int _domain)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3702634851u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterTemperature cPUIDSDK_fp_GetDisplayAdapterTemperature = (CPUIDSDK_fp_GetDisplayAdapterTemperature)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterTemperature));
				return cPUIDSDK_fp_GetDisplayAdapterTemperature(objptr, _adapter_index, _domain);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetDisplayAdapterFanSpeed(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4178440731u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterFanSpeed cPUIDSDK_fp_GetDisplayAdapterFanSpeed = (CPUIDSDK_fp_GetDisplayAdapterFanSpeed)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanSpeed));
				return cPUIDSDK_fp_GetDisplayAdapterFanSpeed(objptr, _adapter_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetDisplayAdapterFanPWM(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4121946975u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterFanPWM cPUIDSDK_fp_GetDisplayAdapterFanPWM = (CPUIDSDK_fp_GetDisplayAdapterFanPWM)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanPWM));
				return cPUIDSDK_fp_GetDisplayAdapterFanPWM(objptr, _adapter_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public bool GetDisplayAdapterMemoryType(int _adapter_index, ref int _type)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(356919948u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterMemoryType cPUIDSDK_fp_GetDisplayAdapterMemoryType = (CPUIDSDK_fp_GetDisplayAdapterMemoryType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryType));
				int num = cPUIDSDK_fp_GetDisplayAdapterMemoryType(objptr, _adapter_index, ref _type);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public bool GetDisplayAdapterMemorySize(int _adapter_index, ref int _size)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3816933123u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterMemorySize cPUIDSDK_fp_GetDisplayAdapterMemorySize = (CPUIDSDK_fp_GetDisplayAdapterMemorySize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemorySize));
				int num = cPUIDSDK_fp_GetDisplayAdapterMemorySize(objptr, _adapter_index, ref _size);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public bool GetDisplayAdapterMemoryBusWidth(int _adapter_index, ref int _bus_width)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1000109880u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth cPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth = (CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth));
				int num = cPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth(objptr, _adapter_index, ref _bus_width);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public string GetDisplayAdapterMemoryVendor(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1444981825u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterMemoryVendor cPUIDSDK_fp_GetDisplayAdapterMemoryVendor = (CPUIDSDK_fp_GetDisplayAdapterMemoryVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryVendor));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterMemoryVendor(objptr, _adapter_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetDisplayAdaterDriverVersion(int _adapter_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2593731890u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterDriverVersion cPUIDSDK_fp_GetDisplayAdapterDriverVersion = (CPUIDSDK_fp_GetDisplayAdapterDriverVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterDriverVersion));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterDriverVersion(objptr, _adapter_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetDirectXVersion()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3567626571u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDirectXVersion cPUIDSDK_fp_GetDirectXVersion = (CPUIDSDK_fp_GetDirectXVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDirectXVersion));
				IntPtr ptr = cPUIDSDK_fp_GetDirectXVersion(objptr);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public bool GetDisplayAdapterBusInfos(int _adapter_index, ref int _bus_type, ref int _multi_vpu)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3710106183u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterBusInfos cPUIDSDK_fp_GetDisplayAdapterBusInfos = (CPUIDSDK_fp_GetDisplayAdapterBusInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterBusInfos));
				int num = cPUIDSDK_fp_GetDisplayAdapterBusInfos(objptr, _adapter_index, ref _bus_type, ref _multi_vpu);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public string GetDisplayAdapterCoreFamily(int _adapter_index, ref int _core)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(848717100u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDisplayAdapterCoreFamily cPUIDSDK_fp_GetDisplayAdapterCoreFamily = (CPUIDSDK_fp_GetDisplayAdapterCoreFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCoreFamily));
				IntPtr ptr = cPUIDSDK_fp_GetDisplayAdapterCoreFamily(objptr, _adapter_index, ref _core);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetNumberOfMonitors()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(533348244u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfMonitors cPUIDSDK_fp_GetNumberOfMonitors = (CPUIDSDK_fp_GetNumberOfMonitors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfMonitors));
				return cPUIDSDK_fp_GetNumberOfMonitors(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetMonitorName(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2851296230u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorName cPUIDSDK_fp_GetMonitorName = (CPUIDSDK_fp_GetMonitorName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorName));
				IntPtr ptr = cPUIDSDK_fp_GetMonitorName(objptr, _monitor_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMonitorVendor(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(103943268u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorVendor cPUIDSDK_fp_GetMonitorVendor = (CPUIDSDK_fp_GetMonitorVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorVendor));
				IntPtr ptr = cPUIDSDK_fp_GetMonitorVendor(objptr, _monitor_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMonitorID(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2241792830u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorID cPUIDSDK_fp_GetMonitorID = (CPUIDSDK_fp_GetMonitorID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorID));
				IntPtr ptr = cPUIDSDK_fp_GetMonitorID(objptr, _monitor_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetMonitorSerial(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1998515773u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorSerial cPUIDSDK_fp_GetMonitorSerial = (CPUIDSDK_fp_GetMonitorSerial)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorSerial));
				IntPtr ptr = cPUIDSDK_fp_GetMonitorSerial(objptr, _monitor_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public bool GetMonitorManufacturingDate(int _monitor_index, ref int _week, ref int _year)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3537741243u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorManufacturingDate cPUIDSDK_fp_GetMonitorManufacturingDate = (CPUIDSDK_fp_GetMonitorManufacturingDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorManufacturingDate));
				int num = cPUIDSDK_fp_GetMonitorManufacturingDate(objptr, _monitor_index, ref _week, ref _year);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public float GetMonitorSize(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(278405424u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorSize cPUIDSDK_fp_GetMonitorSize = (CPUIDSDK_fp_GetMonitorSize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorSize));
				return cPUIDSDK_fp_GetMonitorSize(objptr, _monitor_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public bool GetMonitorResolution(int _monitor_index, ref int _width, ref int _height, ref int _frequency)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4047102579u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorResolution cPUIDSDK_fp_GetMonitorResolution = (CPUIDSDK_fp_GetMonitorResolution)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorResolution));
				int num = cPUIDSDK_fp_GetMonitorResolution(objptr, _monitor_index, ref _width, ref _height, ref _frequency);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public int GetMonitorMaxPixelClock(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3862154343u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorMaxPixelClock cPUIDSDK_fp_GetMonitorMaxPixelClock = (CPUIDSDK_fp_GetMonitorMaxPixelClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorMaxPixelClock));
				return cPUIDSDK_fp_GetMonitorMaxPixelClock(objptr, _monitor_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public float GetMonitorGamma(int _monitor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3382809411u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetMonitorGamma cPUIDSDK_fp_GetMonitorGamma = (CPUIDSDK_fp_GetMonitorGamma)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorGamma));
				return cPUIDSDK_fp_GetMonitorGamma(objptr, _monitor_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetNumberOfStorageDevice()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2361203066u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfStorageDevice cPUIDSDK_fp_GetNumberOfStorageDevice = (CPUIDSDK_fp_GetNumberOfStorageDevice)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfStorageDevice));
				return cPUIDSDK_fp_GetNumberOfStorageDevice(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetStorageDriveNumber(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2418614354u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDriveNumber cPUIDSDK_fp_GetStorageDriveNumber = (CPUIDSDK_fp_GetStorageDriveNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDriveNumber));
				return cPUIDSDK_fp_GetStorageDriveNumber(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetStorageDeviceName(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1356374449u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceName cPUIDSDK_fp_GetStorageDeviceName = (CPUIDSDK_fp_GetStorageDeviceName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceName));
				IntPtr ptr = cPUIDSDK_fp_GetStorageDeviceName(objptr, _index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetStorageDeviceRevision(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2998625654u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceRevision cPUIDSDK_fp_GetStorageDeviceRevision = (CPUIDSDK_fp_GetStorageDeviceRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceRevision));
				IntPtr ptr = cPUIDSDK_fp_GetStorageDeviceRevision(objptr, _index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public string GetStorageDeviceSerialNumber(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1170246529u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceSerialNumber cPUIDSDK_fp_GetStorageDeviceSerialNumber = (CPUIDSDK_fp_GetStorageDeviceSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceSerialNumber));
				IntPtr ptr = cPUIDSDK_fp_GetStorageDeviceSerialNumber(objptr, _index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetStorageDeviceBusType(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1415489725u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceBusType cPUIDSDK_fp_GetStorageDeviceBusType = (CPUIDSDK_fp_GetStorageDeviceBusType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceBusType));
				return cPUIDSDK_fp_GetStorageDeviceBusType(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetStorageDeviceRotationSpeed(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2795851082u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceRotationSpeed cPUIDSDK_fp_GetStorageDeviceRotationSpeed = (CPUIDSDK_fp_GetStorageDeviceRotationSpeed)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceRotationSpeed));
				return cPUIDSDK_fp_GetStorageDeviceRotationSpeed(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public uint GetStorageDeviceFeatureFlag(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1862458885u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceFeatureFlag cPUIDSDK_fp_GetStorageDeviceFeatureFlag = (CPUIDSDK_fp_GetStorageDeviceFeatureFlag)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceFeatureFlag));
				return (uint)cPUIDSDK_fp_GetStorageDeviceFeatureFlag(objptr, _index);
			}
			return (uint)I_UNDEFINED_VALUE;
		}
		catch
		{
			return (uint)I_UNDEFINED_VALUE;
		}
	}

	public int GetStorageDeviceNumberOfVolumes(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(147329424u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes cPUIDSDK_fp_GetStorageDeviceNumberOfVolumes = (CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes));
				return cPUIDSDK_fp_GetStorageDeviceNumberOfVolumes(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetStorageDeviceVolumeLetter(int _index, int _volume_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1385997625u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceVolumeLetter cPUIDSDK_fp_GetStorageDeviceVolumeLetter = (CPUIDSDK_fp_GetStorageDeviceVolumeLetter)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeLetter));
				IntPtr ptr = cPUIDSDK_fp_GetStorageDeviceVolumeLetter(objptr, _index, _volume_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public float GetStorageDeviceVolumeTotalCapacity(int _index, int _volume_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2792967410u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity cPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity = (CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity));
				return cPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity(objptr, _index, _volume_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public float GetStorageDeviceVolumeAvailableCapacity(int _index, int _volume_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1269995365u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity cPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity = (CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity));
				return cPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity(objptr, _index, _volume_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public bool GetStorageDeviceSmartAttribute(int _hdd_index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(519585264u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceSmartAttribute cPUIDSDK_fp_GetStorageDeviceSmartAttribute = (CPUIDSDK_fp_GetStorageDeviceSmartAttribute)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceSmartAttribute));
				int num = cPUIDSDK_fp_GetStorageDeviceSmartAttribute(objptr, _hdd_index, _attrib_index, ref _id, ref _flags, ref _value, ref _worst, _data);
				if (num == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public int GetStorageDevicePowerOnHours(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2179924958u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDevicePowerOnHours cPUIDSDK_fp_GetStorageDevicePowerOnHours = (CPUIDSDK_fp_GetStorageDevicePowerOnHours)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDevicePowerOnHours));
				return cPUIDSDK_fp_GetStorageDevicePowerOnHours(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetStorageDevicePowerCycleCount(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3333131607u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDevicePowerCycleCount cPUIDSDK_fp_GetStorageDevicePowerCycleCount = (CPUIDSDK_fp_GetStorageDevicePowerCycleCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDevicePowerCycleCount));
				return cPUIDSDK_fp_GetStorageDevicePowerCycleCount(objptr, _index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public float GetStorageDeviceTotalCapacity(int _index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1059356232u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetStorageDeviceTotalCapacity cPUIDSDK_fp_GetStorageDeviceTotalCapacity = (CPUIDSDK_fp_GetStorageDeviceTotalCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceTotalCapacity));
				return cPUIDSDK_fp_GetStorageDeviceTotalCapacity(objptr, _index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}

	public int GetNumberOfDevices()
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2212825034u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfDevices cPUIDSDK_fp_GetNumberOfDevices = (CPUIDSDK_fp_GetNumberOfDevices)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfDevices));
				return cPUIDSDK_fp_GetNumberOfDevices(objptr);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public int GetDeviceClass(int _device_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1751961817u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDeviceClass cPUIDSDK_fp_GetDeviceClass = (CPUIDSDK_fp_GetDeviceClass)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDeviceClass));
				return cPUIDSDK_fp_GetDeviceClass(objptr, _device_index);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public string GetDeviceName(int _device_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1164872413u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetDeviceName cPUIDSDK_fp_GetDeviceName = (CPUIDSDK_fp_GetDeviceName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDeviceName));
				IntPtr ptr = cPUIDSDK_fp_GetDeviceName(objptr, _device_index);
				string result = Marshal.PtrToStringAnsi(ptr);
				Marshal.FreeBSTR(ptr);
				return result;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	public int GetNumberOfSensors(int _device_index, int _sensor_class)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1873993573u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetNumberOfSensors cPUIDSDK_fp_GetNumberOfSensors = (CPUIDSDK_fp_GetNumberOfSensors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfSensors));
				return cPUIDSDK_fp_GetNumberOfSensors(objptr, _device_index, _sensor_class);
			}
			return I_UNDEFINED_VALUE;
		}
		catch
		{
			return I_UNDEFINED_VALUE;
		}
	}

	public bool GetSensorInfos(int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref string _szName, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4212782643u);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr _szNamePtr = IntPtr.Zero;
				CPUIDSDK_fp_GetSensorInfos cPUIDSDK_fp_GetSensorInfos = (CPUIDSDK_fp_GetSensorInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSensorInfos));
				int num = cPUIDSDK_fp_GetSensorInfos(objptr, _device_index, _sensor_index, _sensor_class, ref _sensor_id, ref _szNamePtr, ref _raw_value, ref _value, ref _min_value, ref _max_value);
				if (num == 1)
				{
					_szName = Marshal.PtrToStringAnsi(_szNamePtr);
					Marshal.FreeBSTR(_szNamePtr);
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	public void SensorClearMinMax(int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref string _szName, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2771995250u);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr zero = IntPtr.Zero;
				CPUIDSDK_fp_SensorClearMinMax cPUIDSDK_fp_SensorClearMinMax = (CPUIDSDK_fp_SensorClearMinMax)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_SensorClearMinMax));
				cPUIDSDK_fp_SensorClearMinMax(objptr, _device_index, _sensor_index, _sensor_class);
			}
		}
		catch
		{
		}
	}

	public float GetSensorTypeValue(int _sensor_type, ref int _device_index, ref int _sensor_index)
	{
		try
		{
			IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2690072750u);
			if (intPtr != IntPtr.Zero)
			{
				CPUIDSDK_fp_GetSensorTypeValue cPUIDSDK_fp_GetSensorTypeValue = (CPUIDSDK_fp_GetSensorTypeValue)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSensorTypeValue));
				return cPUIDSDK_fp_GetSensorTypeValue(objptr, _sensor_type, ref _device_index, ref _sensor_index);
			}
			return F_UNDEFINED_VALUE;
		}
		catch
		{
			return F_UNDEFINED_VALUE;
		}
	}
}
