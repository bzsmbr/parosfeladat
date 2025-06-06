﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class cities3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @$"
        insert into [City]
                    (PostalCode, 
	                Name)	
                values
                    (7600, 'Pécs'),
	(7639, 'Kökény'),
	(7661, 'Erzsébet'),
	(7661, 'Kátoly'),
	(7661, 'Kékesd'),
	(7661, 'Szellő'),
	(7663, 'Máriakéménd'),
	(7664, 'Berkesd'),
	(7664, 'Pereked'),
	(7664, 'Szilágy'),
	(7666, 'Pogány'),
	(7668, 'Keszü'),
	(7668, 'Gyód'),
	(7671, 'Bicsérd'),
	(7671, 'Aranyosgadány'),
	(7671, 'Zók'),
	(7672, 'Boda'),
	(7673, 'Kővágószőlős'),
	(7673, 'Cserkút'),
	(7675, 'Bakonya'),
	(7675, 'Kővágótöttös'),
	(7677, 'Orfű'),
	(7678, 'Abaliget'),
	(7678, 'Husztót'),
	(7678, 'Kovácsszénája'),
	(7681, 'Hetvehely'),
	(7681, 'Okorvölgy'),
	(7681, 'Szentkatalin'),
	(7682, 'Bükkösd'),
	(7683, 'Helesfa'),
	(7683, 'Cserdi'),
	(7683, 'Dinnyeberki'),
	(7691, 'Pécs Vasas'),
	(7693, 'Pécs Hird'),
	(7694, 'Hosszúhetény'),
	(7695, 'Mecseknádasd'),
	(7695, 'Óbánya'),
	(7695, 'Ófalu'),
	(7696, 'Hidas'),
	(7700, 'Mohács'),
	(7711, 'Bár'),
	(7712, 'Dunaszekcső'),
	(7714, 'Mohács Újmohács'),
	(7715, 'Mohács Sárhát'),
	(7716, 'Homorúd'),
	(7717, 'Kölked'),
	(7718, 'Udvar'),
	(7720, 'Pécsvárad'),
	(7720, 'Apátvarasd'),
	(7720, 'Lovászhetény'),
	(7720, 'Martonfa'),
	(7720, 'Zengővárkony'),
	(7723, 'Erdősmecske'),
	(7724, 'Feked'),
	(7725, 'Szebény'),
	(7726, 'Véménd'),
	(7727, 'Palotabozsok'),
	(7728, 'Somberek'),
	(7728, 'Görcsönydoboka'),
	(7731, 'Nagypall'),
	(7732, 'Fazekasboda'),
	(7733, 'Geresdlak'),
	(7733, 'Maráza'),
	(7735, 'Himesháza'),
	(7735, 'Erdősmárok'),
	(7735, 'Szűr'),
	(7737, 'Székelyszabar'),
	(7741, 'Nagykozár'),
	(7742, 'Bogád'),
	(7743, 'Romonya'),
	(7744, 'Ellend'),
	(7745, 'Olasz'),
	(7745, 'Hásságy'),
	(7747, 'Belvárdgyula'),
	(7747, 'Birján'),
	(7751, 'Szederkény'),
	(7751, 'Monyoród'),
	(7752, 'Versend'),
	(7753, 'Szajk'),
	(7754, 'Bóly'),
	(7755, 'Töttös'),
	(7756, 'Borjád'),
	(7756, 'Kisbudmér'),
	(7756, 'Nagybudmér'),
	(7756, 'Pócsa'),
	(7757, 'Babarc'),
	(7757, 'Liptód'),
	(7759, 'Lánycsók'),
	(7759, 'Kisnyárád'),
	(7761, 'Kozármisleny'),
	(7761, 'Lothárd'),
	(7761, 'Magyarsarlós'),
	(7762, 'Pécsudvard'),
	(7763, 'Egerág'),
	(7763, 'Áta'),
	(7763, 'Kisherend'),
	(7763, 'Szemely'),
	(7763, 'Szőkéd'),
	(7766, 'Újpetre'),
	(7766, 'Kiskassa'),
	(7766, 'Peterd'),
	(7766, 'Pécsdevecser'),
	(7768, 'Vokány'),
	(7768, 'Kistótfalu'),
	(7771, 'Palkonya'),
	(7772, 'Villánykövesd'),
	(7772, 'Ivánbattyán'),
	(7773, 'Villány'),
	(7773, 'Kisjakabfalva'),
	(7774, 'Márok'),
	(7775, 'Magyarbóly'),
	(7775, 'Illocska'),
	(7775, 'Kislippó'),
	(7775, 'Lapáncsa'),
	(7781, 'Lippó'),
	(7781, 'Ivándárda'),
	(7781, 'Sárok'),
	(7782, 'Bezedek'),
	(7783, 'Majs'),
	(7784, 'Nagynyárád'),
	(7785, 'Sátorhely'),
	(7800, 'Siklós'),
	(7800, 'Kisharsány'),
	(7800, 'Nagytótfalu'),
	(7811, 'Szalánta'),
	(7811, 'Bisse'),
	(7811, 'Bosta'),
	(7811, 'Csarnóta'),
	(7811, 'Szilvás'),
	(7811, 'Túrony'),
	(7812, 'Garé'),
	(7813, 'Szava'),
	(7814, 'Ócsárd'),
	(7814, 'Babarcszőlős'),
	(7814, 'Kisdér'),
	(7814, 'Siklósbodony'),
	(7815, 'Harkány'),
	(7817, 'Diósviszló'),
	(7817, 'Márfa'),
	(7817, 'Rádfalva'),
	(7822, 'Nagyharsány'),
	(7823, 'Siklósnagyfalu'),
	(7823, 'Kistapolca'),
	(7824, 'Egyházasharaszti'),
	(7824, 'Old'),
	(7826, 'Alsószentmárton'),
	(7827, 'Beremend'),
	(7827, 'Kásád'),
	(7831, 'Pellérd'),
	(7833, 'Görcsöny'),
	(7833, 'Regenye'),
	(7833, 'Szőke'),
	(7834, 'Baksa'),
	(7834, 'Tengeri'),
	(7834, 'Téseny'),
	(7836, 'Bogádmindszent'),
	(7836, 'Ózdfalu'),
	(7837, 'Hegyszentmárton'),
	(7838, 'Vajszló'),
	(7838, 'Besence'),
	(7838, 'Hirics'),
	(7838, 'Lúzsok'),
	(7838, 'Nagycsány'),
	(7838, 'Páprád'),
	(7838, 'Piskó'),
	(7838, 'Vejti'),
	(7839, 'Zaláta'),
	(7839, 'Kemse'),
	(7841, 'Sámod'),
	(7841, 'Adorjás'),
	(7841, 'Baranyahídvég'),
	(7841, 'Kisszentmárton'),
	(7841, 'Kórós'),
	(7843, 'Kémes'),
	(7843, 'Cún'),
	(7843, 'Drávapiski'),
	(7843, 'Szaporca'),
	(7843, 'Tésenfa'),
	(7846, 'Drávacsepely'),
	(7847, 'Kovácshida'),
	(7847, 'Drávaszerdahely'),
	(7847, 'Ipacsfa'),
	(7849, 'Drávacsehi'),
	(7850, 'Drávapalkonya'),
	(7851, 'Drávaszabolcs'),
	(7853, 'Gordisa'),
	(7854, 'Matty'),
	(7900, 'Szigetvár'),
	(7900, 'Botykapeterd'),
	(7900, 'Csertő'),
	(7912, 'Nagypeterd'),
	(7912, 'Nyugotszenterzsébet'),
	(7912, 'Nagyváty'),
	(7913, 'Szentdénes'),
	(7914, 'Rózsafa'),
	(7914, 'Bánfa'),
	(7914, 'Katádfa'),
	(7915, 'Dencsháza'),
	(7915, 'Szentegát'),
	(7918, 'Lakócsa'),
	(7918, 'Szentborbás'),
	(7918, 'Tótújfalu'),
	(7921, 'Somogyhatvan'),
	(7922, 'Somogyapáti'),
	(7923, 'Basal'),
	(7923, 'Patapoklosi'),
	(7924, 'Somogyviszló'),
	(7925, 'Somogyhárságy'),
	(7925, 'Magyarlukafa'),
	(7926, 'Vásárosbéc'),
	(7932, 'Mozsgó'),
	(7932, 'Almáskeresztúr'),
	(7932, 'Szulimán'),
	(7934, 'Almamellék'),
	(7935, 'Ibafa'),
	(7935, 'Csebény'),
	(7935, 'Horváthertelend'),
	(7936, 'Szentlászló'),
	(7937, 'Boldogasszonyfa'),
	(7940, 'Szentlőrinc'),
	(7940, 'Csonkamindszent'),
	(7940, 'Kacsóta'),
	(7951, 'Szabadszentkirály'),
	(7951, 'Gerde'),
	(7951, 'Pécsbagota'),
	(7951, 'Velény'),
	(7953, 'Királyegyháza'),
	(7954, 'Magyarmecske'),
	(7954, 'Gilvánfa'),
	(7954, 'Gyöngyfa'),
	(7954, 'Kisasszonyfa'),
	(7954, 'Magyartelek'),
	(7957, 'Okorág'),
	(7958, 'Kákics'),
	(7960, 'Sellye'),
	(7960, 'Drávaiványi'),
	(7960, 'Drávasztára'),
	(7960, 'Marócsa'),
	(7960, 'Sósvertike'),
	(7960, 'Sumony'),
	(7964, 'Csányoszró'),
	(7966, 'Bogdása'),
	(7967, 'Drávafok'),
	(7967, 'Drávakeresztúr'),
	(7967, 'Markóc'),
	(7968, 'Felsőszentmárton'),
	(7971, 'Hobol'),
	(7972, 'Gyöngyösmellék'),
	(7973, 'Teklafalu'),
	(7973, 'Bürüs'),
	(7973, 'Endrőc'),
	(7973, 'Várad'),
	(7975, 'Kétújfalu'),
	(7976, 'Zádor'),
	(7976, 'Szörény'),
	(7977, 'Kastélyosdombó'),
	(7977, 'Drávagárdony'),
	(7977, 'Potony'),
	(7979, 'Drávatamási'),
	(7980, 'Pettend'),
	(7981, 'Nemeske'),
	(7981, 'Kistamási'),
	(7981, 'Merenye'),
	(7981, 'Molvány'),
	(7981, 'Tótszentgyörgy'),
	(7985, 'Nagydobsza'),
	(7985, 'Kisdobsza'),
	(7987, 'Istvándi'),
	(7988, 'Darány'),
	(8000, 'Székesfehérvár'),
	(8019, 'Székesfehérvár Börgönd'),
	(8041, 'Csór'),
	(8042, 'Moha'),
	(8043, 'Iszkaszentgyörgy'),
	(8044, 'Kincsesbánya'),
	(8045, 'Isztimér'),
	(8046, 'Bakonykúti'),
	(8051, 'Sárkeresztes'),
	(8052, 'Fehérvárcsurgó'),
	(8053, 'Bodajk'),
	(8054, 'Balinka Eszény'),
	(8055, 'Balinka'),
	(8056, 'Bakonycsernye'),
	(8060, 'Mór'),
	(8065, 'Nagyveleg'),
	(8066, 'Pusztavám'),
	(8071, 'Magyaralmás'),
	(8072, 'Söréd'),
	(8073, 'Csákberény'),
	(8074, 'Csókakő'),
	(8080, 'Bodmér'),
	(8081, 'Zámoly'),
	(8082, 'Gánt'),
	(8083, 'Csákvár'),
	(8085, 'Vértesboglár'),
	(8086, 'Felcsút'),
	(8087, 'Alcsútdoboz'),
	(8088, 'Tabajd'),
	(8089, 'Vértesacsa'),
	(8092, 'Pátka'),
	(8093, 'Lovasberény'),
	(8095, 'Pákozd'),
	(8096, 'Sukoró'),
	(8097, 'Nadap'),
	(8100, 'Várpalota'),
	(8103, 'Várpalota Inota'),
	(8104, 'Várpalota Inotai Erőmű'),
	(8105, 'Pétfürdő'),
	(8109, 'Tés'),
	(8111, 'Seregélyes'),
	(8112, 'Zichyújfalu'),
	(8121, 'Tác'),
	(8122, 'Csősz'),
	(8123, 'Soponya'),
	(8124, 'Káloz'),
	(8125, 'Sárkeresztúr'),
	(8126, 'Sárszentágota'),
	(8127, 'Aba'),
	(8130, 'Enying'),
	(8131, 'Enying Balatonbozsok'),
	(8132, 'Lepsény'),
	(8133, 'Mezőszentgyörgy'),
	(8134, 'Mátyásdomb'),
	(8135, 'Dég'),
	(8136, 'Lajoskomárom'),
	(8137, 'Mezőkomárom'),
	(8138, 'Szabadhídvég'),
	(8142, 'Úrhida'),
	(8143, 'Sárszentmihály'),
	(8144, 'Sárkeszi'),
	(8145, 'Nádasdladány'),
	(8146, 'Jenő'),
	(8151, 'Szabadbattyán'),
	(8152, 'Kőszárhegy'),
	(8154, 'Polgárdi'),
	(8156, 'Kisláng'),
	(8157, 'Füle'),
	(8161, 'Ősi'),
	(8162, 'Küngös'),
	(8163, 'Csajág'),
	(8164, 'Balatonfőkajár'),
	(8171, 'Balatonaliga'),
	(8171, 'Balatonaliga Balatonvilágos'),
	(8172, 'Balatonkenese Balatonakarattya'),
	(8173, 'Balatonkenese Üdülőtelep'),
	(8174, 'Balatonkenese'),
	(8175, 'Balatonfűzfő'),
	(8181, 'Berhida'),
	(8182, 'Berhida Peremartongyártelep'),
	(8183, 'Papkeszi'),
	(8184, 'Balatonfűzfő Fűzfőgyártelep'),
	(8191, 'Öskü'),
	(8192, 'Hajmáskér'),
	(8193, 'Sóly'),
	(8194, 'Vilonya'),
	(8195, 'Királyszentistván'),
	(8196, 'Litér'),
	(8200, 'Veszprém'),
	(8220, 'Balatonalmádi'),
	(8225, 'Szentkirályszabadja'),
	(8226, 'Alsóörs'),
	(8227, 'Felsőörs'),
	(8228, 'Lovas'),
	(8229, 'Csopak'),
	(8229, 'Paloznak'),
	(8230, 'Balatonfüred'),
	(8230, 'Balatonszőlős'),
	(8236, 'Balatonfüred Balatonarács'),
	(8237, 'Tihany'),
	(8241, 'Aszófő'),
	(8242, 'Balatonudvari'),
	(8242, 'Örvényes'),
	(8243, 'Balatonakali'),
	(8244, 'Dörgicse'),
	(8245, 'Pécsely'),
	(8245, 'Vászoly'),
	(8246, 'Tótvázsony'),
	(8247, 'Hidegkút'),
	(8248, 'Nemesvámos'),
	(8248, 'Veszprémfajsz'),
	(8251, 'Zánka'),
	(8252, 'Balatonszepezd'),
	(8253, 'Révfülöp'),
	(8254, 'Kővágóörs'),
	(8254, 'Kékkút'),
	(8255, 'Balatonrendes'),
	(8256, 'Ábrahámhegy'),
	(8256, 'Salföld'),
	(8257, 'Badacsonytomaj Badacsonyörs'),
	(8258, 'Badacsonytomaj'),
	(8261, 'Badacsonytomaj Badacsony'),
	(8263, 'Badacsonytördemic'),
	(8264, 'Szigliget'),
	(8265, 'Hegymagas'),
	(8271, 'Mencshely'),
	(8272, 'Szentantalfa'),
	(8272, 'Balatoncsicsó'),
	(8272, 'Óbudavár'),
	(8272, 'Szentjakabfa'),
	(8272, 'Tagyon'),
	(8273, 'Monoszló'),
	(8274, 'Köveskál'),
	(8275, 'Balatonhenye'),
	(8281, 'Szentbékkálla'),
	(8282, 'Mindszentkálla'),
	(8283, 'Káptalantóti'),
	(8284, 'Nemesgulács'),
	(8284, 'Kisapáti'),
	(8286, 'Gyulakeszi'),
	(8291, 'Nagyvázsony'),
	(8291, 'Barnag'),
	(8291, 'Pula'),
	(8291, 'Vöröstó'),
	(8292, 'Öcs'),
	(8294, 'Kapolcs'),
	(8294, 'Vigántpetend'),
	(8295, 'Taliándörögd'),
	(8296, 'Monostorapáti'),
	(8296, 'Hegyesd'),
	(8297, 'Tapolca Diszel'),
	(8300, 'Tapolca'),
	(8300, 'Raposka'),
	(8308, 'Zalahaláp'),
	(8308, 'Sáska'),
	(8311, 'Nemesvita'),
	(8312, 'Balatonederics'),
	(8313, 'Balatongyörök'),
	(8314, 'Vonyarcvashegy'),
	(8315, 'Gyenesdiás'),
	(8316, 'Várvölgy'),
	(8316, 'Vállus'),
	(8318, 'Lesencetomaj'),
	(8318, 'Lesencefalu'),
	(8319, 'Lesenceistvánd'),
	(8321, 'Uzsa'),
	(8330, 'Sümeg'),
	(8341, 'Mihályfa'),
	(8341, 'Kisvásárhely'),
	(8341, 'Szalapa'),
	(8342, 'Óhíd'),
	(8344, 'Zalaerdőd'),
	(8344, 'Hetyefő'),
	(8345, 'Dabronc'),
	(8346, 'Gógánfa'),
	(8347, 'Ukk'),
	(8348, 'Rigács'),
	(8348, 'Megyer'),
	(8348, 'Zalameggyes'),
	(8349, 'Zalagyömörő'),
	(8351, 'Sümegprága'),
	(8352, 'Bazsi'),
	(8353, 'Zalaszántó'),
	(8353, 'Vindornyalak'),
	(8354, 'Karmacs'),
	(8354, 'Vindornyafok'),
	(8354, 'Zalaköveskút'),
	(8355, 'Vindornyaszőlős'),
	(8356, 'Kisgörbő'),
	(8356, 'Nagygörbő'),
	(8357, 'Sümegcsehi'),
	(8357, 'Döbröce'),
	(8360, 'Keszthely'),
	(8371, 'Nemesbük'),
	(8372, 'Cserszegtomaj'),
	(8373, 'Rezi'),
	(8380, 'Hévíz'),
	(8380, 'Felsőpáhok'),
	(8391, 'Sármellék'),
	(8392, 'Zalavár'),
	(8393, 'Szentgyörgyvár'),
	(8394, 'Alsópáhok'),
	(8400, 'Ajka'),
	(8409, 'Úrkút'),
	(8411, 'Veszprém Kádárta'),
	(8412, 'Veszprém Gyulafirátót'),
	(8413, 'Eplény'),
	(8414, 'Olaszfalu'),
	(8415, 'Nagyesztergár'),
	(8416, 'Dudar'),
	(8417, 'Csetény'),
	(8418, 'Bakonyoszlop'),
	(8419, 'Csesznek'),
	(8420, 'Zirc'),
	(8422, 'Bakonynána'),
	(8423, 'Szápár'),
	(8424, 'Jásd'),
	(8425, 'Lókút'),
	(8426, 'Pénzesgyőr'),
	(8427, 'Bakonybél'),
	(8428, 'Borzavár'),
	(8429, 'Porva'),
	(8430, 'Bakonyszentkirály'),
	(8431, 'Bakonyszentlászló'),
	(8432, 'Fenyőfő'),
	(8433, 'Bakonygyirót'),
	(8434, 'Románd'),
	(8435, 'Gic'),
	(8438, 'Veszprémvarsány'),
	(8439, 'Sikátor'),
	(8440, 'Herend'),
	(8441, 'Márkó'),
	(8442, 'Hárskút'),
	(8443, 'Bánd'),
	(8444, 'Szentgál'),
	(8445, 'Városlőd'),
	(8445, 'Csehbánya'),
	(8446, 'Kislőd'),
	(8447, 'Ajka Ajkarendek'),
	(8448, 'Ajka Bakonygyepes'),
	(8449, 'Magyarpolány'),
	(8451, 'Ajka Padragkút'),
	(8452, 'Halimba'),
	(8452, 'Szőc'),
	(8454, 'Nyirád'),
	(8455, 'Pusztamiske'),
	(8456, 'Noszlop'),
	(8457, 'Bakonypölöske'),
	(8458, 'Oroszi'),
	(8460, 'Devecser'),
	(8468, 'Kolontár'),
	(8469, 'Kamond'),
	(8471, 'Káptalanfa'),
	(8471, 'Bodorfa'),
	(8471, 'Nemeshany'),
	(8473, 'Gyepükaján'),
	(8474, 'Csabrendek'),
	(8475, 'Veszprémgalsa'),
	(8475, 'Hosztót'),
	(8475, 'Szentimrefalva'),
	(8476, 'Zalaszegvár'),
	(8477, 'Tüskevár'),
	(8477, 'Apácatorna'),
	(8477, 'Kisberzseny'),
	(8478, 'Somlójenő'),
	(8479, 'Borszörcsök'),
	(8481, 'Somlóvásárhely'),
	(8482, 'Doba'),
	(8483, 'Somlószőlős'),
	(8483, 'Kisszőlős'),
	(8484, 'Nagyalásony'),
	(8484, 'Somlóvecse'),
	(8484, 'Vid'),
	(8485, 'Dabrony'),
	(8491, 'Karakószörcsök'),
	(8492, 'Kerta'),
	(8493, 'Iszkáz'),
	(8494, 'Kiscsősz'),
	(8495, 'Csögle'),
	(8496, 'Nagypirit'),
	(8496, 'Kispirit'),
	(8497, 'Adorjánháza'),
	(8497, 'Egeralja'),
	(8500, 'Pápa'),
	(8511, 'Pápa Borsosgyőr'),
	(8512, 'Nyárád'),
	(8513, 'Mihályháza'),
	(8514, 'Mezőlak'),
	(8515, 'Békás'),
	(8516, 'Kemeneshőgyész'),
	(8517, 'Magyargencs'),
	(8518, 'Kemenesszentpéter'),
	(8521, 'Nagyacsád'),
	(8522, 'Nemesgörzsöny'),
	(8523, 'Egyházaskesző'),
	(8523, 'Várkesző'),
	(8531, 'Marcaltő Ihász'),
	(8532, 'Marcaltő'),
	(8533, 'Malomsok'),
	(8541, 'Takácsi'),
	(8542, 'Vaszar'),
	(8543, 'Gecse'),
	(8551, 'Nagygyimót'),
	(8552, 'Vanyola'),
	(8553, 'Lovászpatona'),
	(8554, 'Nagydém'),
	(8555, 'Bakonytamási'),
	(8556, 'Pápateszér'),
	(8557, 'Bakonyszentiván'),
	(8557, 'Bakonyság'),
	(8558, 'Csót'),
	(8561, 'Adásztevel'),
	(8562, 'Nagytevel'),
	(8563, 'Homokbödöge'),
	(8564, 'Ugod'),
	(8565, 'Béb'),
	(8571, 'Bakonykoppány'),
	(8572, 'Bakonyszücs'),
	(8581, 'Bakonyjákó'),
	(8581, 'Németbánya'),
	(8582, 'Farkasgyepű'),
	(8591, 'Pápa Kéttornyúlak'),
	(8591, 'Nóráp'),
	(8592, 'Dáka'),
	(8593, 'Pápadereske'),
	(8594, 'Pápasalamon'),
	(8595, 'Kup'),
	(8596, 'Pápakovácsi'),
	(8597, 'Ganna'),
	(8597, 'Döbrönte'),
	(8598, 'Pápa Tapolcafő'),
	(8600, 'Siófok'),
	(8609, 'Siófok Balatonszéplak'),
	(8611, 'Siófok Balatonkiliti'),
	(8612, 'Nyim'),
	(8613, 'Balatonendréd'),
	(8614, 'Bálványos'),
	(8617, 'Kőröshegy'),
	(8618, 'Kereki'),
	(8619, 'Pusztaszemes'),
	(8621, 'Zamárdi'),
	(8622, 'Szántód'),
	(8623, 'Balatonföldvár'),
	(8624, 'Balatonszárszó'),
	(8625, 'Szólád'),
	(8626, 'Teleki'),
	(8627, 'Kötcse'),
	(8628, 'Nagycsepely'),
	(8630, 'Balatonboglár'),
	(8635, 'Ordacsehi'),
	(8636, 'Balatonszemes'),
	(8637, 'Balatonőszöd'),
	(8638, 'Balatonlelle'),
	(8640, 'Fonyód'),
	(8646, 'Balatonfenyves'),
	(8647, 'Balatonmáriafürdő'),
	(8648, 'Balatonkeresztúr'),
	(8649, 'Balatonberény'),
	(8651, 'Balatonszabadi'),
	(8652, 'Siójut'),
	(8653, 'Ádánd'),
	(8654, 'Ságvár'),
	(8655, 'Som'),
	(8656, 'Nagyberény'),
	(8658, 'Bábonymegyer'),
	(8660, 'Tab'),
	(8660, 'Lulla'),
	(8660, 'Sérsekszőlős'),
	(8660, 'Torvaj'),
	(8660, 'Zala'),
	(8666, 'Bedegkér'),
	(8666, 'Somogyegres'),
	(8667, 'Kánya'),
	(8668, 'Tengőd'),
	(8669, 'Miklósi'),
	(8671, 'Kapoly'),
	(8672, 'Zics'),
	(8673, 'Somogymeggyes'),
	(8674, 'Nágocs'),
	(8675, 'Andocs'),
	(8676, 'Karád'),
	(8681, 'Látrány'),
	(8681, 'Visz'),
	(8683, 'Somogytúr'),
	(8684, 'Somogybabod'),
	(8685, 'Gamás'),
	(8691, 'Balatonboglár Szőlőskislak'),
	(8692, 'Szőlősgyörök'),
	(8692, 'Gyugy'),
	(8693, 'Lengyeltóti'),
	(8693, 'Kisberény'),
	(8694, 'Hács'),
	(8695, 'Buzsák'),
	(8696, 'Táska'),
	(8697, 'Öreglak'),
	(8698, 'Somogyvár'),
	(8698, 'Pamuk'),
	(8699, 'Somogyvámos'),
	(8700, 'Marcali'),
	(8700, 'Csömend'),
	(8705, 'Somogyszentpál'),
	(8706, 'Nikla'),
	(8707, 'Pusztakovácsi'),
	(8707, 'Libickozma'),
	(8708, 'Somogyfajsz'),
	(8709, 'Marcali Horvátkút'),
	(8710, 'Balatonszentgyörgy'),
	(8711, 'Vörs'),
	(8712, 'Balatonújlak'),
	(8713, 'Kéthely'),
	(8714, 'Marcali Bize'),
	(8714, 'Kelevíz'),
	(8716, 'Mesztegnyő'),
	(8716, 'Hosszúvíz'),
	(8716, 'Gadány'),
	(8717, 'Szenyér'),
	(8717, 'Nemeskisfalud'),
	(8718, 'Tapsony'),
	(8719, 'Böhönye'),
	(8721, 'Vése'),
	(8722, 'Nemesdéd'),
	(8723, 'Varászló'),
	(8724, 'Inke'),
	(8725, 'Iharosberény'),
	(8726, 'Iharos'),
	(8726, 'Somogycsicsó'),
	(8728, 'Pogányszentpéter'),
	(8731, 'Hollád'),
	(8731, 'Tikos'),
	(8732, 'Sávoly'),
	(8732, 'Főnyed'),
	(8732, 'Szegerdő'),
	(8733, 'Somogysámson'),
	(8734, 'Somogyzsitfa'),
	(8735, 'Csákány'),
	(8736, 'Szőkedencs'),
	(8737, 'Somogysimonyi'),
	(8738, 'Nemesvid'),
	(8739, 'Nagyszakácsi'),
	(8741, 'Zalaapáti'),
	(8741, 'Bókaháza'),
	(8742, 'Esztergályhorváti'),
	(8743, 'Zalaszabar'),
	(8744, 'Orosztony'),
	(8745, 'Kerecseny'),
	(8746, 'Nagyrada'),
	(8747, 'Garabonc'),
	(8747, 'Zalamerenye'),
	(8749, 'Zalakaros'),
	(8751, 'Zalakomár Kiskomárom'),
	(8752, 'Zalakomár Komárváros'),
	(8753, 'Balatonmagyaród'),
	(8754, 'Galambok'),
	(8756, 'Nagyrécse'),
	(8756, 'Csapi'),
	(8756, 'Kisrécse'),
	(8756, 'Zalasárszeg'),
	(8761, 'Pacsa'),
	(8761, 'Zalaigrice'),
	(8762, 'Szentpéterúr'),
	(8762, 'Gétye'),
	(8764, 'Dióskál'),
	(8764, 'Zalaszentmárton'),
	(8765, 'Egeraracsa'),
	(8767, 'Felsőrajk'),
	(8767, 'Alsórajk'),
	(8767, 'Pötréte'),
	(8771, 'Hahót'),
	(8772, 'Zalaszentbalázs'),
	(8772, 'Börzönce'),
	(8773, 'Pölöskefő'),
	(8773, 'Kacorlak'),
	(8774, 'Gelse'),
	(8774, 'Gelsesziget'),
	(8774, 'Kilimán'),
	(8776, 'Magyarszerdahely'),
	(8776, 'Bocska'),
	(8776, 'Magyarszentmiklós'),
	(8777, 'Hosszúvölgy'),
	(8777, 'Fűzvölgy'),
	(8777, 'Homokkomárom'),
	(8778, 'Újudvar'),
	(8782, 'Zalacsány'),
	(8782, 'Ligetfalva'),
	(8782, 'Tilaj'),
	(8784, 'Kehidakustány'),
	(8785, 'Zalaszentgrót Zalakoppány'),
	(8785, 'Kallósd'),
	(8788, 'Zalaszentlászló'),
	(8788, 'Sénye'),
	(8789, 'Zalaszentgrót Zalaudvarnok'),
	(8790, 'Zalaszentgrót'),
	(8792, 'Zalavég'),
	(8793, 'Zalaszentgrót Tekenye'),
	(8795, 'Zalaszentgrót Csáford'),
	(8796, 'Türje'),
	(8797, 'Batyk'),
	(8798, 'Zalabér'),
	(8799, 'Pakod'),
	(8799, 'Dötk'),
	(8800, 'Nagykanizsa'),
	(8808, 'Nagykanizsa Palin'),
	(8809, 'Nagykanizsa Sánc'),
	(8821, 'Nagybakónak'),
	(8822, 'Zalaújlak'),
	(8824, 'Sand'),
	(8825, 'Miháld'),
	(8825, 'Pat'),
	(8827, 'Zalaszentjakab'),
	(8831, 'Nagykanizsa Miklósfa'),
	(8831, 'Liszó'),
	(8834, 'Murakeresztúr'),
	(8835, 'Fityeház'),
	(8840, 'Csurgó'),
	(8840, 'Csurgónagymarton'),
	(8849, 'Szenta'),
	(8851, 'Gyékényes'),
	(8852, 'Zákány'),
	(8853, 'Zákányfalu'),
	(8854, 'Őrtilos'),
	(8855, 'Belezna'),
	(8856, 'Surd'),
	(8857, 'Nemespátró'),
	(8858, 'Porrog'),
	(8858, 'Porrogszentkirály'),
	(8858, 'Porrogszentpál'),
	(8858, 'Somogybükkösd'),
	(8861, 'Szepetnek'),
	(8862, 'Semjénháza'),
	(8863, 'Molnári'),
	(8864, 'Tótszerdahely'),
	(8865, 'Tótszentmárton'),
	(8866, 'Becsehely'),
	(8866, 'Petrivente'),
	(8868, 'Letenye'),
	(8868, 'Kistolmács'),
	(8868, 'Murarátka'),
	(8868, 'Zajk'),
	(8872, 'Muraszemenye'),
	(8872, 'Szentmargitfalva'),
	(8873, 'Csörnyeföld'),
	(8874, 'Dobri'),
	(8874, 'Kerkaszentkirály'),
	(8876, 'Tormafölde'),
	(8877, 'Tornyiszentmiklós'),
	(8878, 'Lovászi'),
	(8879, 'Szécsisziget'),
	(8879, 'Kerkateskánd'),
	(8881, 'Sormás'),
	(8882, 'Eszteregnye'),
	(8883, 'Rigyác'),
	(8885, 'Borsfa'),
	(8885, 'Valkonya'),
	(8886, 'Oltárc'),
	(8887, 'Bázakerettye'),
	(8887, 'Lasztonya'),
	(8888, 'Lispeszentadorján'),
	(8888, 'Kiscsehi'),
	(8888, 'Maróc'),
	(8891, 'Bánokszentgyörgy'),
	(8891, 'Várfölde'),
	(8893, 'Szentliszló'),
	(8893, 'Bucsuta'),
	(8895, 'Pusztamagyaród'),
	(8896, 'Pusztaszentlászló'),
	(8897, 'Söjtör'),
	(8900, 'Zalaegerszeg'),
	(8904, 'Zalaegerszeg Andráshida'),
	(8908, 'Zalaegerszeg Szanatórium'),
	(8911, 'Nagykutas'),
	(8911, 'Kiskutas'),
	(8912, 'Kispáli'),
	(8912, 'Nagypáli'),
	(8913, 'Egervár'),
	(8913, 'Gősfa'),
	(8913, 'Lakhegy'),
	(8914, 'Vasboldogasszony'),
	(8915, 'Nemesrádó'),
	(8917, 'Milejszeg'),
	(8918, 'Csonkahegyhát'),
	(8918, 'Németfalu'),
	(8919, 'Kustánszeg'),
	(8921, 'Zalaszentiván'),
	(8921, 'Alibánfa'),
	(8921, 'Pethőhenye'),
	(8921, 'Zalaszentlőrinc'),
	(8923, 'Nemesapáti'),
	(8924, 'Alsónemesapáti'),
	(8925, 'Búcsúszentlászló'),
	(8925, 'Kisbucsa'),
	(8925, 'Nemeshetés'),
	(8925, 'Nemessándorháza'),
	(8925, 'Nemesszentandrás'),
	(8929, 'Pölöske'),
	(8931, 'Kemendollár'),
	(8931, 'Vöckönd'),
	(8932, 'Pókaszepetk'),
	(8932, 'Gyűrűs'),
	(8932, 'Zalaistvánd'),
	(8934, 'Bezeréd'),
	(8935, 'Nagykapornak'),
	(8935, 'Almásháza'),
	(8935, 'Misefa'),
	(8935, 'Orbányosfa'),
	(8935, 'Padár'),
	(8936, 'Zalaszentmihály'),
	(8943, 'Bocfölde'),
	(8943, 'Csatár'),
	(8944, 'Sárhida'),
	(8945, 'Bak'),
	(8946, 'Tófej'),
	(8946, 'Baktüttös'),
	(8946, 'Pusztaederics'),
	(8947, 'Zalatárnok'),
	(8947, 'Szentkozmadombja'),
	(8948, 'Nova'),
	(8948, 'Barlahida'),
	(8949, 'Mikekarácsonyfa'),
	(8951, 'Gutorfölde'),
	(8951, 'Csertalakos'),
	(8953, 'Szentpéterfölde'),
	(8954, 'Ortaháza'),
	(8956, 'Páka'),
	(8956, 'Kányavár'),
	(8956, 'Pördefölde'),
	(8957, 'Csömödér'),
	(8957, 'Hernyék'),
	(8957, 'Kissziget'),
	(8957, 'Zebecke'),
	(8958, 'Iklódbördőce'),
	(8960, 'Lenti'),
	(8966, 'Lenti Lentikápolna'),
	(8969, 'Gáborjánháza'),
	(8969, 'Bödeháza'),
	(8969, 'Szijártóháza'),
	(8969, 'Zalaszombatfa'),
	(8971, 'Zalabaksa'),
	(8971, 'Kerkabarabás'),
	(8973, 'Csesztreg'),
	(8973, 'Alsószenterzsébet'),
	(8973, 'Felsőszenterzsébet'),
	(8973, 'Kerkafalva'),
	(8973, 'Kerkakutas'),
	(8973, 'Magyarföld'),
	(8973, 'Ramocsa'),
	(8975, 'Szentgyörgyvölgy'),
	(8976, 'Nemesnép'),
	(8976, 'Márokföld'),
	(8977, 'Resznek'),
	(8977, 'Baglad'),
	(8977, 'Lendvajakabfa'),
	(8978, 'Rédics'),
	(8978, 'Belsősárd'),
	(8978, 'Gosztola'),
	(8978, 'Külsősárd'),
	(8978, 'Lendvadedes'),
	(8981, 'Gellénháza'),
	(8981, 'Lickóvadamos'),
	(8983, 'Nagylengyel'),
	(8983, 'Babosdöbréte'),
	(8983, 'Ormándlak'),
	(8984, 'Petrikeresztúr'),
	(8984, 'Gombosszeg'),
	(8984, 'Iborfia'),
	(8985, 'Becsvölgye'),
	(8986, 'Pórszombat'),
	(8986, 'Pusztaapáti'),
	(8986, 'Szilvágy'),
	(8988, 'Kálócfa'),
	(8988, 'Kozmadombja'),
	(8989, 'Dobronhegy'),
	(8990, 'Pálfiszeg'),
	(8991, 'Teskánd'),
	(8991, 'Böde'),
	(8991, 'Hottó'),
	(8992, 'Bagod'),
	(8992, 'Boncodfölde'),
	(8992, 'Hagyárosbörönd'),
	(8992, 'Zalaboldogfa'),
	(8994, 'Zalaszentgyörgy'),
	(8994, 'Kávás'),
	(8995, 'Salomvár'),
	(8995, 'Keménfa'),
	(8996, 'Zalacséb'),
	(8997, 'Zalaháshágy'),
	(8998, 'Vaspör'),
	(8998, 'Ozmánbük'),
	(8999, 'Zalalövő'),
	(8999, 'Csöde'),
	(9000, 'Győr'),
	(9010, 'Győr Bácsa'),
	(9011, 'Győr Győrszentiván'),
	(9012, 'Győr Ménfőcsanak'),
	(9019, 'Győr Gyirmót'),
	(9061, 'Vámosszabadi'),
	(9062, 'Kisbajcs'),
	(9062, 'Vének'),
	(9063, 'Nagybajcs'),
	(9071, 'Gönyű'),
	(9072, 'Nagyszentjános'),
	(9073, 'Bőny'),
	(9074, 'Rétalap'),
	(9081, 'Győrújbarát'),
	(9082, 'Nyúl'),
	(9083, 'Écs'),
	(9084, 'Győrság'),
	(9085, 'Pázmándfalu'),
	(9086, 'Töltéstava'),
	(9088, 'Bakonypéterd'),
	(9089, 'Lázi'),
	(9090, 'Pannonhalma'),
	(9091, 'Ravazd'),
	(9092, 'Tarjánpuszta'),
	(9093, 'Győrasszonyfa')
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

