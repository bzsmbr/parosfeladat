﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class cities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @$"
                insert into [City]
                    (PostalCode, 
	                Name)	
                values
                    (4074, 'Hajdúböszörmény Nagypród'),
	(4075, 'Görbeháza'),
	(4078, 'Debrecen Haláp'),
	(4079, 'Debrecen Bánk'),
	(4080, 'Hajdúnánás'),
	(4085, 'Hajdúnánás Tedej'),
	(4086, 'Hajdúböszörmény Hajdúvid'),
	(4087, 'Hajdúdorog'),
	(4090, 'Polgár'),
	(4090, 'Folyás'),
	(4096, 'Újtikos'),
	(4097, 'Tiszagyulaháza'),
	(4100, 'Berettyóújfalu'),
	(4103, 'Berettyóújfalu Berettyószentmárton'),
	(4110, 'Biharkeresztes'),
	(4114, 'Bojt'),
	(4115, 'Ártánd'),
	(4116, 'Berekböszörmény'),
	(4117, 'Told'),
	(4118, 'Mezőpeterd'),
	(4119, 'Váncsod'),
	(4121, 'Szentpéterszeg'),
	(4122, 'Gáborján'),
	(4123, 'Hencida'),
	(4124, 'Esztár'),
	(4125, 'Pocsaj'),
	(4126, 'Kismarja'),
	(4127, 'Nagykereki'),
	(4128, 'Bedő'),
	(4130, 'Derecske'),
	(4132, 'Tépe'),
	(4133, 'Konyár'),
	(4134, 'Mezősas'),
	(4135, 'Körösszegapáti'),
	(4136, 'Körösszakál'),
	(4137, 'Magyarhomorog'),
	(4138, 'Komádi'),
	(4141, 'Furta'),
	(4142, 'Zsáka'),
	(4143, 'Vekerd'),
	(4144, 'Darvas'),
	(4145, 'Csökmő'),
	(4146, 'Újiráz'),
	(4150, 'Püspökladány'),
	(4161, 'Báránd'),
	(4162, 'Szerep Hosszúhát'),
	(4163, 'Szerep'),
	(4164, 'Bakonszeg'),
	(4171, 'Sárrétudvari'),
	(4172, 'Biharnagybajom'),
	(4173, 'Nagyrábé'),
	(4174, 'Bihartorda'),
	(4175, 'Bihardancsháza'),
	(4176, 'Sáp'),
	(4177, 'Földes'),
	(4181, 'Nádudvar'),
	(4183, 'Kaba'),
	(4184, 'Tetétlen'),
	(4200, 'Hajdúszoboszló'),
	(4211, 'Ebes'),
	(4212, 'Hajdúszovát'),
	(4220, 'Hajdúböszörmény'),
	(4224, 'Hajdúböszörmény Bodaszőlő'),
	(4225, 'Debrecen Józsa'),
	(4231, 'Bököny'),
	(4232, 'Geszteréd'),
	(4233, 'Balkány'),
	(4234, 'Szakoly'),
	(4235, 'Biri'),
	(4241, 'Bocskaikert'),
	(4242, 'Hajdúhadház'),
	(4243, 'Téglás'),
	(4244, 'Újfehértó'),
	(4245, 'Érpatak'),
	(4246, 'Nyíregyháza Butykatelep'),
	(4251, 'Hajdúsámson'),
	(4252, 'Nyíradony Tamásipuszta'),
	(4253, 'Nyíradony Aradványpuszta'),
	(4254, 'Nyíradony'),
	(4262, 'Nyíracsád'),
	(4263, 'Nyírmártonfalva'),
	(4264, 'Nyírábrány'),
	(4266, 'Fülöp'),
	(4267, 'Penészlek'),
	(4271, 'Mikepércs'),
	(4272, 'Sáránd'),
	(4273, 'Hajdúbagos'),
	(4274, 'Hosszúpályi'),
	(4275, 'Monostorpályi'),
	(4281, 'Létavértes Nagyléta'),
	(4283, 'Létavértes Vértes'),
	(4284, 'Kokad'),
	(4285, 'Álmosd'),
	(4286, 'Bagamér'),
	(4287, 'Vámospércs'),
	(4288, 'Újléta'),
	(4300, 'Nyírbátor'),
	(4311, 'Nyírgyulaj'),
	(4320, 'Nagykálló'),
	(4324, 'Kállósemjén'),
	(4325, 'Kisléta'),
	(4326, 'Máriapócs'),
	(4327, 'Pócspetri'),
	(4331, 'Nyírcsászári'),
	(4332, 'Nyírderzs'),
	(4333, 'Nyírkáta'),
	(4334, 'Hodász'),
	(4335, 'Kántorjánosi'),
	(4336, 'Őr'),
	(4337, 'Jármi'),
	(4338, 'Papos'),
	(4341, 'Nyírvasvári'),
	(4342, 'Terem'),
	(4343, 'Bátorliget'),
	(4351, 'Vállaj'),
	(4352, 'Mérk'),
	(4353, 'Tiborszállás'),
	(4354, 'Fábiánháza'),
	(4355, 'Nagyecsed'),
	(4356, 'Nyírcsaholy'),
	(4361, 'Nyírbogát'),
	(4362, 'Nyírgelse'),
	(4363, 'Nyírmihálydi'),
	(4371, 'Nyírlugos'),
	(4372, 'Nyírbéltek'),
	(4373, 'Ömböly'),
	(4374, 'Encsencs'),
	(4375, 'Piricse'),
	(4376, 'Nyírpilis'),
	(4400, 'Nyíregyháza'),
	(4405, 'Nyíregyháza Borbánya'),
	(4431, 'Nyíregyháza Sóstófürdő'),
	(4432, 'Nyíregyháza Nyírszőlős'),
	(4433, 'Nyíregyháza Felsősima'),
	(4434, 'Kálmánháza'),
	(4440, 'Tiszavasvári'),
	(4441, 'Szorgalmatos'),
	(4445, 'Nagycserkesz'),
	(4446, 'Tiszaeszlár Bashalom'),
	(4447, 'Tiszalök Kisfástanya'),
	(4450, 'Tiszalök'),
	(4455, 'Tiszadada'),
	(4456, 'Tiszadob'),
	(4461, 'Nyírtelek'),
	(4463, 'Tiszanagyfalu'),
	(4464, 'Tiszaeszlár'),
	(4465, 'Rakamaz'),
	(4466, 'Timár'),
	(4467, 'Szabolcs'),
	(4468, 'Balsa'),
	(4471, 'Gávavencsellő Gáva'),
	(4472, 'Gávavencsellő Vencsellő'),
	(4474, 'Tiszabercel'),
	(4475, 'Paszab'),
	(4481, 'Nyíregyháza Sóstóhegy'),
	(4482, 'Kótaj'),
	(4483, 'Buj'),
	(4484, 'Ibrány'),
	(4485, 'Nagyhalász'),
	(4486, 'Tiszatelek Kétérköz'),
	(4487, 'Tiszatelek'),
	(4488, 'Beszterec'),
	(4491, 'Újdombrád'),
	(4492, 'Dombrád'),
	(4493, 'Tiszakanyár'),
	(4494, 'Kékcse'),
	(4495, 'Döge'),
	(4496, 'Szabolcsveresmart'),
	(4501, 'Kemecse'),
	(4502, 'Vasmegyer'),
	(4503, 'Tiszarád'),
	(4511, 'Nyírbogdány'),
	(4515, 'Kék'),
	(4516, 'Demecser'),
	(4517, 'Gégény'),
	(4521, 'Berkesz'),
	(4522, 'Nyírtass'),
	(4523, 'Pátroha'),
	(4524, 'Ajak'),
	(4525, 'Rétközberencs'),
	(4531, 'Nyírpazony'),
	(4532, 'Nyírtura'),
	(4533, 'Sényő'),
	(4534, 'Székely'),
	(4535, 'Nyíribrony'),
	(4536, 'Ramocsaháza'),
	(4537, 'Nyírkércs'),
	(4541, 'Nyírjákó'),
	(4542, 'Petneháza'),
	(4543, 'Laskod'),
	(4544, 'Nyírkarász'),
	(4545, 'Gyulaháza'),
	(4546, 'Anarcs'),
	(4547, 'Szabolcsbáka'),
	(4551, 'Nyíregyháza Oros'),
	(4552, 'Napkor'),
	(4553, 'Apagy'),
	(4554, 'Nyírtét'),
	(4555, 'Levelek'),
	(4556, 'Magy'),
	(4557, 'Besenyőd'),
	(4558, 'Ófehértó'),
	(4561, 'Baktalórántháza'),
	(4562, 'Vaja'),
	(4563, 'Rohod'),
	(4564, 'Nyírmada'),
	(4565, 'Pusztadobos'),
	(4566, 'Ilk'),
	(4567, 'Gemzse'),
	(4600, 'Kisvárda'),
	(4611, 'Jéke'),
	(4621, 'Fényeslitke'),
	(4622, 'Komoró'),
	(4623, 'Tuzsér'),
	(4624, 'Tiszabezdéd'),
	(4625, 'Záhony'),
	(4625, 'Győröcske'),
	(4627, 'Zsurk'),
	(4628, 'Tiszaszentmárton'),
	(4631, 'Pap'),
	(4632, 'Nyírlövő'),
	(4633, 'Lövőpetri'),
	(4634, 'Aranyosapáti'),
	(4635, 'Újkenéz'),
	(4641, 'Mezőladány'),
	(4642, 'Tornyospálca'),
	(4643, 'Benk'),
	(4644, 'Mándok'),
	(4645, 'Tiszamogyorós'),
	(4646, 'Eperjeske'),
	(4700, 'Mátészalka'),
	(4721, 'Szamoskér'),
	(4722, 'Nyírmeggyes'),
	(4731, 'Tunyogmatolcs'),
	(4732, 'Cégénydányád'),
	(4733, 'Gyügye'),
	(4734, 'Szamosújlak'),
	(4735, 'Szamossályi'),
	(4735, 'Hermánszeg'),
	(4737, 'Kisnamény'),
	(4737, 'Darnó'),
	(4741, 'Jánkmajtis'),
	(4742, 'Csegöld'),
	(4743, 'Csengersima'),
	(4745, 'Szamosbecs'),
	(4746, 'Szamostatárfalva'),
	(4751, 'Kocsord'),
	(4752, 'Győrtelek'),
	(4754, 'Géberjén'),
	(4754, 'Fülpösdaróc'),
	(4755, 'Ököritófülpös'),
	(4756, 'Rápolt'),
	(4761, 'Porcsalma'),
	(4762, 'Tyukod'),
	(4763, 'Ura'),
	(4764, 'Csengerújfalu'),
	(4765, 'Csenger'),
	(4765, 'Komlódtótfalu'),
	(4766, 'Pátyod'),
	(4767, 'Szamosangyalos'),
	(4800, 'Vásárosnamény'),
	(4803, 'Vásárosnamény Gergelyiugornya'),
	(4804, 'Vásárosnamény Vitka'),
	(4811, 'Kisvarsány'),
	(4812, 'Nagyvarsány'),
	(4813, 'Gyüre'),
	(4821, 'Ópályi'),
	(4822, 'Nyírparasznya'),
	(4823, 'Nagydobos'),
	(4824, 'Szamosszeg'),
	(4826, 'Olcsva'),
	(4831, 'Tiszaszalka'),
	(4832, 'Tiszavid'),
	(4833, 'Tiszaadony'),
	(4834, 'Tiszakerecseny'),
	(4835, 'Mátyus'),
	(4836, 'Lónya'),
	(4841, 'Jánd'),
	(4842, 'Gulács'),
	(4843, 'Hetefejércse'),
	(4844, 'Csaroda'),
	(4845, 'Tákos'),
	(4900, 'Fehérgyarmat'),
	(4911, 'Nábrád'),
	(4912, 'Kérsemjén'),
	(4913, 'Panyola'),
	(4914, 'Olcsvaapáti'),
	(4921, 'Kisar'),
	(4921, 'Tivadar'),
	(4922, 'Nagyar'),
	(4931, 'Tarpa'),
	(4932, 'Márokpapi'),
	(4933, 'Beregsurány'),
	(4934, 'Beregdaróc'),
	(4935, 'Gelénes'),
	(4936, 'Vámosatya'),
	(4937, 'Barabás'),
	(4941, 'Penyige'),
	(4942, 'Mánd'),
	(4942, 'Nemesborzova'),
	(4943, 'Kömörő'),
	(4944, 'Túristvándi'),
	(4945, 'Szatmárcseke'),
	(4946, 'Tiszakóród'),
	(4947, 'Tiszacsécse'),
	(4948, 'Milota'),
	(4951, 'Tiszabecs'),
	(4952, 'Uszka'),
	(4953, 'Magosliget'),
	(4954, 'Sonkád'),
	(4955, 'Botpalád'),
	(4956, 'Kispalád'),
	(4961, 'Zsarolyán'),
	(4962, 'Nagyszekeres'),
	(4963, 'Kisszekeres'),
	(4964, 'Fülesd'),
	(4965, 'Kölcse'),
	(4966, 'Vámosoroszi'),
	(4967, 'Csaholc'),
	(4968, 'Túrricse'),
	(4969, 'Tisztaberek'),
	(4971, 'Rozsály'),
	(4972, 'Gacsály'),
	(4973, 'Császló'),
	(4974, 'Zajta'),
	(4975, 'Méhtelek'),
	(4976, 'Garbolc'),
	(4977, 'Nagyhódos'),
	(4977, 'Kishódos'),
	(5000, 'Szolnok'),
	(5008, 'Szolnok Szandaszőllős'),
	(5051, 'Zagyvarékas'),
	(5052, 'Újszász'),
	(5053, 'Szászberek'),
	(5054, 'Jászalsószentgyörgy'),
	(5055, 'Jászladány'),
	(5061, 'Tiszasüly'),
	(5062, 'Kőtelek'),
	(5063, 'Hunyadfalva'),
	(5064, 'Csataszög'),
	(5065, 'Nagykörű'),
	(5071, 'Besenyszög'),
	(5081, 'Szajol'),
	(5082, 'Tiszatenyő'),
	(5083, 'Kengyel'),
	(5084, 'Rákócziújfalu'),
	(5085, 'Rákóczifalva'),
	(5091, 'Tószeg'),
	(5092, 'Tiszavárkony'),
	(5093, 'Vezseny'),
	(5094, 'Tiszajenő'),
	(5095, 'Tiszavárkony Tiszavárkonyi-Szőlők'),
	(5100, 'Jászberény'),
	(5111, 'Jászfelsőszentgyörgy'),
	(5121, 'Jászjákóhalma'),
	(5122, 'Jászdózsa'),
	(5123, 'Jászárokszállás'),
	(5124, 'Jászágó'),
	(5125, 'Pusztamonostor'),
	(5126, 'Jászfényszaru'),
	(5130, 'Jászapáti'),
	(5135, 'Jászivány'),
	(5136, 'Jászszentandrás'),
	(5137, 'Jászkisér'),
	(5141, 'Jásztelek'),
	(5142, 'Alattyán'),
	(5143, 'Jánoshida'),
	(5144, 'Jászboldogháza'),
	(5152, 'Jászberény Portelek'),
	(5200, 'Törökszentmiklós'),
	(5211, 'Tiszapüspöki'),
	(5212, 'Törökszentmiklós Surjány'),
	(5213, 'Fegyvernek Szapárfalu'),
	(5222, 'Örményes'),
	(5231, 'Fegyvernek'),
	(5232, 'Tiszabő'),
	(5233, 'Tiszagyenda'),
	(5234, 'Tiszaroff'),
	(5235, 'Tiszabura'),
	(5241, 'Abádszalók'),
	(5243, 'Tiszaderzs'),
	(5244, 'Tiszaszőlős'),
	(5300, 'Karcag'),
	(5309, 'Berekfürdő'),
	(5310, 'Kisújszállás'),
	(5321, 'Kunmadaras'),
	(5322, 'Tiszaszentimre'),
	(5323, 'Tiszaszentimre Újszentgyörgy'),
	(5324, 'Tomajmonostora'),
	(5331, 'Kenderes'),
	(5340, 'Kunhegyes'),
	(5349, 'Kenderes Bánhalma'),
	(5350, 'Tiszafüred'),
	(5358, 'Tiszafüred Tiszaörvény'),
	(5359, 'Tiszafüred Kócsújfalu'),
	(5361, 'Tiszaigar'),
	(5362, 'Tiszaörs'),
	(5363, 'Nagyiván'),
	(5400, 'Mezőtúr'),
	(5411, 'Kétpó'),
	(5412, 'Kuncsorba'),
	(5420, 'Túrkeve'),
	(5430, 'Tiszaföldvár'),
	(5435, 'Martfű'),
	(5440, 'Kunszentmárton'),
	(5449, 'Kunszentmárton Kungyalu'),
	(5451, 'Öcsöd'),
	(5452, 'Mesterszállás'),
	(5453, 'Mezőhék'),
	(5461, 'Tiszaföldvár Homok'),
	(5462, 'Cibakháza'),
	(5463, 'Nagyrév'),
	(5464, 'Tiszainoka'),
	(5465, 'Cserkeszőlő'),
	(5471, 'Tiszakürt'),
	(5474, 'Tiszasas'),
	(5475, 'Csépa'),
	(5476, 'Szelevény'),
	(5500, 'Gyomaendrőd'),
	(5501, 'Gyomaendrőd Gyoma'),
	(5502, 'Gyomaendrőd Endrőd'),
	(5510, 'Dévaványa'),
	(5515, 'Ecsegfalva'),
	(5516, 'Körösladány'),
	(5520, 'Szeghalom'),
	(5525, 'Füzesgyarmat'),
	(5526, 'Kertészsziget'),
	(5527, 'Bucsa'),
	(5530, 'Vésztő'),
	(5534, 'Okány'),
	(5536, 'Körösújfalu'),
	(5537, 'Zsadány'),
	(5538, 'Biharugra'),
	(5539, 'Körösnagyharsány'),
	(5540, 'Szarvas'),
	(5551, 'Csabacsűd'),
	(5552, 'Kardos'),
	(5553, 'Kondoros'),
	(5555, 'Hunya'),
	(5556, 'Örménykút'),
	(5561, 'Békésszentandrás'),
	(5600, 'Békéscsaba'),
	(5609, 'Csabaszabadi'),
	(5621, 'Csárdaszállás'),
	(5622, 'Köröstarcsa'),
	(5623, 'Békéscsaba Gerla'),
	(5624, 'Doboz'),
	(5630, 'Békés'),
	(5641, 'Tarhos'),
	(5643, 'Bélmegyer'),
	(5650, 'Mezőberény'),
	(5661, 'Újkígyós'),
	(5662, 'Csanádapáca'),
	(5663, 'Medgyesbodzás'),
	(5664, 'Medgyesbodzás Gábortelep'),
	(5665, 'Pusztaottlaka'),
	(5666, 'Medgyesegyháza'),
	(5667, 'Magyarbánhegyes'),
	(5668, 'Nagybánhegyes'),
	(5671, 'Békéscsaba Mezőmegyer'),
	(5672, 'Murony'),
	(5673, 'Kamut'),
	(5674, 'Kétsoprony'),
	(5675, 'Telekgerendás'),
	(5700, 'Gyula'),
	(5703, 'Gyula József Attila szanatórium'),
	(5711, 'Gyula Gyulavári'),
	(5712, 'Szabadkígyós'),
	(5720, 'Sarkad'),
	(5722, 'Sarkad Cukorgyár'),
	(5725, 'Kötegyán'),
	(5726, 'Méhkerék'),
	(5727, 'Újszalonta'),
	(5731, 'Sarkadkeresztúr'),
	(5732, 'Mezőgyán'),
	(5734, 'Geszt'),
	(5741, 'Kétegyháza'),
	(5742, 'Elek'),
	(5743, 'Lőkösháza'),
	(5744, 'Kevermes'),
	(5745, 'Dombiratos'),
	(5746, 'Kunágota'),
	(5747, 'Almáskamarás'),
	(5751, 'Nagykamarás'),
	(5752, 'Medgyesegyháza Bánkút'),
	(5800, 'Mezőkovácsháza'),
	(5811, 'Végegyháza'),
	(5820, 'Mezőhegyes'),
	(5830, 'Battonya'),
	(5836, 'Dombegyház'),
	(5837, 'Kisdombegyház'),
	(5838, 'Magyardombegyház'),
	(5900, 'Orosháza'),
	(5903, 'Orosháza Rákóczitelep'),
	(5904, 'Orosháza Gyopárosfürdő'),
	(5905, 'Orosháza Szentetornya'),
	(5919, 'Pusztaföldvár'),
	(5920, 'Csorvás'),
	(5925, 'Gerendás'),
	(5931, 'Nagyszénás'),
	(5932, 'Gádoros'),
	(5940, 'Tótkomlós'),
	(5945, 'Kardoskút'),
	(5946, 'Békéssámson'),
	(5948, 'Kaszaper'),
	(6000, 'Kecskemét'),
	(6008, 'Kecskemét Méntelek'),
	(6031, 'Szentkirály'),
	(6032, 'Nyárlőrinc'),
	(6033, 'Városföld'),
	(6034, 'Helvécia'),
	(6035, 'Ballószög'),
	(6041, 'Kerekegyháza'),
	(6042, 'Fülöpháza'),
	(6043, 'Kunbaracs'),
	(6044, 'Kecskemét Hetényegyháza'),
	(6045, 'Ladánybene'),
	(6050, 'Lajosmizse'),
	(6055, 'Felsőlajos'),
	(6060, 'Tiszakécske'),
	(6062, 'Tiszakécske Tiszabög'),
	(6064, 'Tiszaug'),
	(6065, 'Lakitelek'),
	(6066, 'Tiszaalpár Alpár'),
	(6067, 'Tiszaalpár Tiszaújfalú'),
	(6070, 'Izsák'),
	(6075, 'Páhi'),
	(6076, 'Ágasegyháza'),
	(6077, 'Orgovány'),
	(6078, 'Jakabszállás'),
	(6080, 'Szabadszállás'),
	(6085, 'Fülöpszállás'),
	(6086, 'Szalkszentmárton'),
	(6087, 'Dunavecse'),
	(6088, 'Apostag'),
	(6090, 'Kunszentmiklós'),
	(6096, 'Kunpeszér'),
	(6097, 'Kunadacs'),
	(6098, 'Tass'),
	(6100, 'Kiskunfélegyháza'),
	(6111, 'Gátér'),
	(6112, 'Pálmonostora'),
	(6113, 'Petőfiszállás'),
	(6114, 'Bugac'),
	(6114, 'Bugacpusztaháza'),
	(6115, 'Kunszállás'),
	(6116, 'Fülöpjakab'),
	(6120, 'Kiskunmajsa'),
	(6131, 'Szank'),
	(6132, 'Móricgát'),
	(6133, 'Jászszentlászló'),
	(6134, 'Kömpöc'),
	(6135, 'Csólyospálos'),
	(6136, 'Harkakötöny'),
	(6200, 'Kiskőrös'),
	(6211, 'Kaskantyú'),
	(6221, 'Akasztó'),
	(6222, 'Csengőd'),
	(6223, 'Soltszentimre'),
	(6224, 'Tabdi'),
	(6230, 'Soltvadkert'),
	(6235, 'Bócsa'),
	(6236, 'Tázlár'),
	(6237, 'Kecel'),
	(6238, 'Imrehegy'),
	(6239, 'Császártöltés'),
	(6300, 'Kalocsa'),
	(6311, 'Öregcsertő'),
	(6320, 'Solt'),
	(6321, 'Újsolt'),
	(6323, 'Dunaegyháza'),
	(6325, 'Dunatetétlen'),
	(6326, 'Harta'),
	(6327, 'Harta Állampuszta'),
	(6328, 'Dunapataj'),
	(6331, 'Foktő'),
	(6332, 'Uszód'),
	(6333, 'Dunaszentbenedek'),
	(6334, 'Géderlak'),
	(6335, 'Ordas'),
	(6336, 'Szakmár'),
	(6337, 'Újtelek'),
	(6341, 'Homokmégy'),
	(6342, 'Drágszél'),
	(6343, 'Miske'),
	(6344, 'Hajós'),
	(6345, 'Nemesnádudvar'),
	(6346, 'Sükösd'),
	(6347, 'Érsekcsanád'),
	(6348, 'Érsekhalma'),
	(6351, 'Bátya'),
	(6352, 'Fajsz'),
	(6353, 'Dusnok'),
	(6400, 'Kiskunhalas'),
	(6411, 'Zsana'),
	(6412, 'Balotaszállás'),
	(6413, 'Kunfehértó'),
	(6414, 'Pirtó'),
	(6421, 'Kisszállás'),
	(6422, 'Tompa'),
	(6423, 'Kelebia'),
	(6424, 'Csikéria'),
	(6425, 'Bácsszőlős'),
	(6430, 'Bácsalmás'),
	(6435, 'Kunbaja'),
	(6440, 'Jánoshalma'),
	(6444, 'Kéleshalom'),
	(6445, 'Borota'),
	(6446, 'Rém'),
	(6447, 'Felsőszentiván'),
	(6448, 'Csávoly'),
	(6449, 'Mélykút'),
	(6451, 'Tataháza'),
	(6452, 'Mátételke'),
	(6453, 'Bácsbokod'),
	(6454, 'Bácsborsód'),
	(6455, 'Katymár'),
	(6456, 'Madaras'),
	(6500, 'Baja'),
	(6503, 'Baja Bajaszentistván'),
	(6511, 'Bácsszentgyörgy'),
	(6512, 'Szeremle'),
	(6513, 'Dunafalva'),
	(6521, 'Vaskút'),
	(6522, 'Gara'),
	(6523, 'Csátalja'),
	(6524, 'Dávod'),
	(6525, 'Hercegszántó'),
	(6527, 'Nagybaracska'),
	(6528, 'Bátmonostor'),
	(6600, 'Szentes'),
	(6612, 'Nagytőke'),
	(6621, 'Derekegyház'),
	(6622, 'Nagymágocs'),
	(6623, 'Árpádhalom'),
	(6624, 'Eperjes'),
	(6625, 'Fábiánsebestyén'),
	(6630, 'Mindszent'),
	(6635, 'Szegvár'),
	(6636, 'Mártély'),
	(6640, 'Csongrád'),
	(6645, 'Felgyő'),
	(6646, 'Tömörkény'),
	(6647, 'Csanytelek'),
	(6648, 'Csongrád Bokros'),
	(6700, 'Szeged'),
	(6710, 'Szeged Szentmihály'),
	(6750, 'Algyő'),
	(6753, 'Szeged Tápé'),
	(6754, 'Újszentiván'),
	(6755, 'Kübekháza'),
	(6756, 'Tiszasziget'),
	(6757, 'Szeged Gyálarét'),
	(6758, 'Röszke'),
	(6760, 'Kistelek'),
	(6762, 'Sándorfalva'),
	(6763, 'Szatymaz'),
	(6764, 'Balástya'),
	(6765, 'Csengele'),
	(6766, 'Dóc'),
	(6767, 'Ópusztaszer'),
	(6768, 'Baks'),
	(6769, 'Pusztaszer'),
	(6771, 'Szeged Szőreg'),
	(6772, 'Deszk'),
	(6773, 'Klárafalva'),
	(6774, 'Ferencszállás'),
	(6775, 'Kiszombor'),
	(6781, 'Domaszék'),
	(6782, 'Mórahalom'),
	(6783, 'Ásotthalom'),
	(6784, 'Öttömös'),
	(6785, 'Pusztamérges'),
	(6786, 'Ruzsa'),
	(6787, 'Zákányszék'),
	(6791, 'Szeged Kiskundorozsma'),
	(6792, 'Zsombó'),
	(6793, 'Forráskút'),
	(6794, 'Üllés'),
	(6795, 'Bordány'),
	(6800, 'Hódmezővásárhely'),
	(6806, 'Hódmezővásárhely Szikáncs'),
	(6821, 'Székkutas'),
	(6900, 'Makó'),
	(6911, 'Királyhegyes'),
	(6912, 'Kövegy'),
	(6913, 'Csanádpalota'),
	(6914, 'Pitvaros'),
	(6915, 'Csanádalberti'),
	(6916, 'Ambrózfalva'),
	(6917, 'Nagyér'),
	(6921, 'Maroslele'),
	(6922, 'Földeák'),
	(6923, 'Óföldeák'),
	(6931, 'Apátfalva'),
	(6932, 'Magyarcsanád'),
	(6933, 'Nagylak'),
	(7000, 'Sárbogárd'),
	(7003, 'Sárbogárd Sárszentmiklós'),
	(7011, 'Alap'),
	(7012, 'Alsószentiván'),
	(7013, 'Cece'),
	(7014, 'Sáregres'),
	(7015, 'Igar'),
	(7017, 'Mezőszilas'),
	(7018, 'Sárbogárd Pusztaegres'),
	(7019, 'Sárbogárd Sárhatvan'),
	(7020, 'Dunaföldvár'),
	(7025, 'Bölcske'),
	(7026, 'Madocsa'),
	(7027, 'Paks Dunakömlőd'),
	(7030, 'Paks'),
	(7038, 'Pusztahencse'),
	(7039, 'Németkér'),
	(7041, 'Vajta'),
	(7042, 'Pálfa'),
	(7043, 'Bikács'),
	(7044, 'Nagydorog'),
	(7045, 'Györköny'),
	(7047, 'Sárszentlőrinc'),
	(7051, 'Kajdacs'),
	(7052, 'Kölesd'),
	(7054, 'Tengelic'),
	(7056, 'Szedres'),
	(7057, 'Medina'),
	(7061, 'Belecska'),
	(7062, 'Keszőhidegkút'),
	(7063, 'Szárazd'),
	(7064, 'Gyönk'),
	(7065, 'Miszla'),
	(7066, 'Udvari'),
	(7067, 'Varsád'),
	(7068, 'Kistormás'),
	(7071, 'Szakadát'),
	(7072, 'Diósberény'),
	(7081, 'Simontornya'),
	(7082, 'Kisszékely'),
	(7083, 'Tolnanémedi'),
	(7084, 'Pincehely'),
	(7085, 'Nagyszékely'),
	(7086, 'Ozora'),
	(7087, 'Fürged'),
	(7090, 'Tamási'),
	(7091, 'Pári'),
	(7092, 'Nagykónyi'),
	(7093, 'Értény'),
	(7094, 'Koppányszántó'),
	(7095, 'Iregszemcse'),
	(7095, 'Újireg'),
	(7097, 'Nagyszokoly'),
	(7098, 'Magyarkeszi'),
	(7099, 'Felsőnyék'),
	(7100, 'Szekszárd'),
	(7121, 'Szálka'),
	(7122, 'Kakasd'),
	(7130, 'Tolna'),
	(7131, 'Tolna Mözs'),
	(7132, 'Bogyiszló'),
	(7133, 'Fadd'),
	(7134, 'Gerjen'),
	(7135, 'Dunaszentgyörgy'),
	(7136, 'Fácánkert'),
	(7140, 'Bátaszék'),
	(7142, 'Pörböly'),
	(7143, 'Őcsény'),
	(7144, 'Decs'),
	(7145, 'Sárpilis'),
	(7146, 'Várdomb'),
	(7147, 'Alsónána'),
	(7148, 'Alsónyék'),
	(7149, 'Báta'),
	(7150, 'Bonyhád'),
	(7158, 'Bonyhádvarasd'),
	(7159, 'Kisdorog'),
	(7161, 'Cikó'),
	(7162, 'Grábóc'),
	(7163, 'Mőcsény'),
	(7164, 'Bátaapáti'),
	(7165, 'Mórágy'),
	(7171, 'Sióagárd'),
	(7172, 'Harc'),
	(7173, 'Zomba'),
	(7174, 'Kéty'),
	(7175, 'Felsőnána'),
	(7176, 'Murga'),
	(7181, 'Tevel'),
	(7182, 'Závod'),
	(7183, 'Kisvejke'),
	(7184, 'Lengyel'),
	(7185, 'Mucsfa'),
	(7186, 'Aparhant'),
	(7186, 'Nagyvejke'),
	(7187, 'Bonyhád Majos'),
	(7188, 'Szárász'),
	(7191, 'Hőgyész'),
	(7192, 'Szakály'),
	(7193, 'Regöly'),
	(7194, 'Kalaznó'),
	(7195, 'Mucsi'),
	(7200, 'Dombóvár'),
	(7211, 'Dalmand'),
	(7212, 'Kocsola'),
	(7213, 'Szakcs'),
	(7214, 'Lápafő'),
	(7214, 'Várong'),
	(7215, 'Nak'),
	(7224, 'Dúzs'),
	(7225, 'Csibrák'),
	(7226, 'Kurd'),
	(7227, 'Gyulaj'),
	(7228, 'Döbrököz'),
	(7251, 'Kapospula'),
	(7252, 'Attala'),
	(7253, 'Csoma'),
	(7253, 'Szabadi'),
	(7255, 'Nagyberki'),
	(7256, 'Kercseliget'),
	(7257, 'Mosdós'),
	(7258, 'Baté'),
	(7258, 'Kaposkeresztúr'),
	(7261, 'Taszár'),
	(7261, 'Kaposhomok'),
	(7271, 'Fonó'),
	(7272, 'Gölle'),
	(7273, 'Büssü'),
	(7274, 'Kazsok'),
	(7275, 'Igal'),
	(7276, 'Somogyszil'),
	(7276, 'Gadács'),
	(7279, 'Kisgyalán'),
	(7281, 'Bonnya'),
	(7282, 'Kisbárapáti'),
	(7282, 'Fiad'),
	(7283, 'Somogyacsa'),
	(7284, 'Somogydöröcske'),
	(7285, 'Törökkoppány'),
	(7285, 'Kára'),
	(7285, 'Szorosad'),
	(7300, 'Komló'),
	(7300, 'Komló Mecsekjánosi'),
	(7304, 'Mánfa'),
	(7305, 'Mecsekpölöske'),
	(7331, 'Liget'),
	(7332, 'Magyaregregy'),
	(7333, 'Kárász'),
	(7333, 'Vékény'),
	(7334, 'Szalatnak'),
	(7334, 'Köblény'),
	(7341, 'Csikóstőttős'),
	(7342, 'Mágocs'),
	(7343, 'Nagyhajmás'),
	(7344, 'Mekényes'),
	(7345, 'Alsómocsolád'),
	(7346, 'Bikal'),
	(7347, 'Egyházaskozár'),
	(7348, 'Hegyhátmaróc'),
	(7348, 'Tófű'),
	(7349, 'Szászvár'),
	(7351, 'Máza'),
	(7352, 'Györe'),
	(7353, 'Izmény'),
	(7354, 'Váralja'),
	(7355, 'Nagymányok'),
	(7356, 'Kismányok'),
	(7357, 'Jágónak'),
	(7361, 'Kaposszekcső'),
	(7362, 'Vásárosdombó'),
	(7362, 'Gerényes'),
	(7362, 'Tarrós'),
	(7370, 'Sásd'),
	(7370, 'Felsőegerszeg'),
	(7370, 'Meződ'),
	(7370, 'Oroszló'),
	(7370, 'Palé'),
	(7370, 'Varga'),
	(7370, 'Vázsnok'),
	(7381, 'Kisvaszar'),
	(7381, 'Ág'),
	(7381, 'Tékes'),
	(7383, 'Tormás'),
	(7383, 'Baranyaszentgyörgy'),
	(7383, 'Szágy'),
	(7384, 'Baranyajenő'),
	(7385, 'Gödre Gödreszentmárton'),
	(7386, 'Gödre Gödrekeresztúr'),
	(7391, 'Mindszentgodisa'),
	(7391, 'Kisbeszterce'),
	(7391, 'Kishajmás'),
	(7393, 'Bakóca'),
	(7394, 'Magyarhertelend'),
	(7394, 'Bodolyabér'),
	(7396, 'Magyarszék'),
	(7400, 'Kaposvár'),
	(7400, 'Orci'),
	(7400, 'Zselickislak'),
	(7409, 'Kaposvár Kaposfüred'),
	(7431, 'Juta'),
	(7432, 'Hetes'),
	(7432, 'Csombárd'),
	(7434, 'Mezőcsokonya'),
	(7435, 'Somogysárd'),
	(7436, 'Újvárfalva'),
	(7439, 'Bodrog'),
	(7441, 'Magyaregres'),
	(7442, 'Várda'),
	(7443, 'Somogyjád'),
	(7443, 'Alsóbogát'),
	(7443, 'Edde'),
	(7444, 'Osztopán'),
	(7452, 'Somogyaszaló'),
	(7453, 'Mernye'),
	(7454, 'Somodor'),
	(7455, 'Somogygeszti'),
	(7456, 'Felsőmocsolád'),
	(7457, 'Ecseny'),
	(7458, 'Polány'),
	(7463, 'Magyaratád'),
	(7463, 'Patalom'),
	(7464, 'Ráksi'),
	(7465, 'Szentgáloskér'),
	(7471, 'Zimány'),
	(7472, 'Szentbalázs'),
	(7472, 'Cserénfa'),
	(7473, 'Gálosfa'),
	(7473, 'Hajmás'),
	(7473, 'Kaposgyarmat'),
	(7474, 'Simonfa'),
	(7474, 'Zselicszentpál'),
	(7475, 'Bőszénfa'),
	(7476, 'Kaposszerdahely'),
	(7477, 'Szenna'),
	(7477, 'Patca'),
	(7477, 'Szilvásszentmárton'),
	(7477, 'Zselickisfalud'),
	(7478, 'Bárdudvarnok'),
	(7479, 'Sántos'),
	(7500, 'Nagyatád'),
	(7511, 'Ötvöskónyi'),
	(7512, 'Mike'),
	(7513, 'Rinyaszentkirály'),
	(7514, 'Tarany'),
	(7515, 'Somogyudvarhely'),
	(7516, 'Berzence'),
	(7517, 'Bolhás'),
	(7521, 'Kaposmérő'),
	(7522, 'Kaposújlak'),
	(7523, 'Kaposfő'),
	(7523, 'Kisasszond'),
	(7524, 'Kiskorpád'),
	(7525, 'Jákó'),
	(7526, 'Csököly'),
	(7527, 'Gige'),
	(7527, 'Rinyakovácsi'),
	(7530, 'Kadarkút'),
	(7530, 'Kőkút'),
	(7532, 'Hencse'),
	(7533, 'Hedrehely'),
	(7533, 'Visnye'),
	(7535, 'Lad'),
	(7536, 'Patosfa'),
	(7537, 'Homokszentgyörgy'),
	(7538, 'Kálmáncsa'),
	(7539, 'Szulok'),
	(7541, 'Kutas'),
	(7542, 'Kisbajom'),
	(7543, 'Beleg'),
	(7544, 'Szabás'),
	(7545, 'Nagykorpád'),
	(7551, 'Lábod'),
	(7552, 'Rinyabesenyő'),
	(7553, 'Görgeteg'),
	(7555, 'Csokonyavisonta'),
	(7556, 'Rinyaújlak'),
	(7557, 'Barcs Somogytarnóca'),
	(7561, 'Nagybajom'),
	(7561, 'Pálmajor'),
	(7562, 'Segesd'),
	(7563, 'Somogyszob'),
	(7564, 'Kaszó'),
	(7570, 'Barcs'),
	(7582, 'Komlósd'),
	(7582, 'Péterhida'),
	(7584, 'Babócsa'),
	(7584, 'Rinyaújnép'),
	(7584, 'Somogyaracs'),
	(7585, 'Háromfa'),
	(7585, 'Bakháza'),
	(7586, 'Bolhó'),
	(7587, 'Heresznye'),
	(7588, 'Vízvár'),
	(7589, 'Bélavár')
            ";

            migrationBuilder.Sql(query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $"truncate table [City]";

            migrationBuilder.Sql(query);
        }
    }
}
