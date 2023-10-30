using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreateEncoding;
using static Globals;

namespace ISUP1.isup
{
    internal class Main1_TestCreateEncoding
    {


        public static string @null = null;
        public static string[] empty = new string[0];

        public static string[] letters = { "a", "e", "i", "o", "u" };

        public static string[] letters_numbers = { "11", "a", "7", "e", "5", "i", "4", "o", "1", "u", "21" };

        public static string[] p = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144", "145", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "160", "161", "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "173", "174", "175", "176", "177", "178", "179", "180", "181", "182", "183", "184", "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", "200", "201", "202", "203", "204", "205", "206", "207", "208", "209", "210", "211", "212", "213", "214", "215", "216", "217", "218", "219", "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "230", "231", "232", "233", "234", "235", "236", "237", "238", "239", "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252", "253", "254", "255", "256", "257", "258", "259", "260", "261", "262", "263", "264", "265", "266", "267", "268", "269", "270", "271", "272", "273", "274", "275", "276", "277", "278", "279", "280", "281", "282", "283", "284", "285", "286", "287", "288", "289", "290", "291", "292", "293", "294", "295", "296", "297", "298", "299", "300", "301", "302", "303", "304", "305", "306", "307", "308", "309", "310", "311", "312", "313", "314", "315", "316", "317", "318", "319", "320", "321", "322", "323", "324", "325", "326", "327", "328", "329", "330", "331", "332", "333", "334", "335", "336", "337", "338", "339", "340", "341", "342", "343", "344", "345", "346", "347", "348", "349", "350", "351", "352", "353", "354", "355", "356", "357", "358", "359", "360", "361", "362", "363", "364", "365", "366", "367", "368", "369", "370", "371", "372", "373", "374", "375", "376", "377", "378", "379", "380", "381", "382", "383", "384", "385", "386", "387", "388", "389", "390", "391", "392", "393", "394", "395", "396", "397", "398", "399", "400", "401", "402", "403", "404", "405", "406", "407", "408", "409", "410", "411", "412", "413", "414", "415", "416", "417", "418", "419", "420", "421", "422", "423", "424", "425", "426", "427", "428", "429", "430", "431", "432", "433", "434", "435", "436", "437", "438", "439", "440", "441", "442", "443", "444", "445", "446", "447", "448", "449", "450", "451", "452", "453", "454", "455", "456", "457", "458", "459", "460", "461", "462", "463", "464", "465", "466", "467", "468", "469", "470", "471", "472", "473", "474", "475", "476", "477", "478", "479", "480", "481", "482", "483", "484", "485", "486", "487", "488", "489", "490", "491", "492", "493", "494", "495", "496", "497", "498", "499", "500", "501", "502", "503", "504", "505", "506", "507", "508", "509", "510", "511", "512", "513", "514", "515", "516", "517", "518", "519", "520", "521", "522", "523", "524", "525", "526", "527", "528", "529", "530", "531", "532", "533", "534", "535", "536", "537", "538", "539", "540", "541", "542", "543", "544", "545", "546", "547", "548", "549", "550", "551", "552", "553", "554", "555", "556", "557", "558", "559", "560", "561", "562", "563", "564", "565", "566", "567", "568", "569", "570", "571", "572", "573", "574", "575", "576", "577", "578", "579", "580", "581", "582", "583", "584", "585", "586", "587", "588", "589", "590", "591", "592", "593", "594", "595", "596", "597", "598", "599", "600" };

        public static string[] n = { "-0", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13", "-14", "-15", "-16", "-17", "-18", "-19", "-20", "-21", "-22", "-23", "-24", "-25", "-26", "-27", "-28", "-29", "-30", "-31", "-32", "-33", "-34", "-35", "-36", "-37", "-38", "-39", "-40", "-41", "-42", "-43", "-44", "-45", "-46", "-47", "-48", "-49", "-50", "-51", "-52", "-53", "-54", "-55", "-56", "-57", "-58", "-59", "-60", "-61", "-62", "-63", "-64", "-65", "-66", "-67", "-68", "-69", "-70", "-71", "-72", "-73", "-74", "-75", "-76", "-77", "-78", "-79", "-80", "-81", "-82", "-83", "-84", "-85", "-86", "-87", "-88", "-89", "-90", "-91", "-92", "-93", "-94", "-95", "-96", "-97", "-98", "-99", "-100", "-101", "-102", "-103", "-104", "-105", "-106", "-107", "-108", "-109", "-110", "-111", "-112", "-113", "-114", "-115", "-116", "-117", "-118", "-119", "-120", "-121", "-122", "-123", "-124", "-125", "-126", "-127", "-128", "-129", "-130", "-131", "-132", "-133", "-134", "-135", "-136", "-137", "-138", "-139", "-140", "-141", "-142", "-143", "-144", "-145", "-146", "-147", "-148", "-149", "-150", "-151", "-152", "-153", "-154", "-155", "-156", "-157", "-158", "-159", "-160", "-161", "-162", "-163", "-164", "-165", "-166", "-167", "-168", "-169", "-170", "-171", "-172", "-173", "-174", "-175", "-176", "-177", "-178", "-179", "-180", "-181", "-182", "-183", "-184", "-185", "-186", "-187", "-188", "-189", "-190", "-191", "-192", "-193", "-194", "-195", "-196", "-197", "-198", "-199", "-200", "-201", "-202", "-203", "-204", "-205", "-206", "-207", "-208", "-209", "-210", "-211", "-212", "-213", "-214", "-215", "-216", "-217", "-218", "-219", "-220", "-221", "-222", "-223", "-224", "-225", "-226", "-227", "-228", "-229", "-230", "-231", "-232", "-233", "-234", "-235", "-236", "-237", "-238", "-239", "-240", "-241", "-242", "-243", "-244", "-245", "-246", "-247", "-248", "-249", "-250", "-251", "-252", "-253", "-254", "-255", "-256", "-257", "-258", "-259", "-260", "-261", "-262", "-263", "-264", "-265", "-266", "-267", "-268", "-269", "-270", "-271", "-272", "-273", "-274", "-275", "-276", "-277", "-278", "-279", "-280", "-281", "-282", "-283", "-284", "-285", "-286", "-287", "-288", "-289", "-290", "-291", "-292", "-293", "-294", "-295", "-296", "-297", "-298", "-299", "-300", "-301", "-302", "-303", "-304", "-305", "-306", "-307", "-308", "-309", "-310", "-311", "-312", "-313", "-314", "-315", "-316", "-317", "-318", "-319", "-320", "-321", "-322", "-323", "-324", "-325", "-326", "-327", "-328", "-329", "-330", "-331", "-332", "-333", "-334", "-335", "-336", "-337", "-338", "-339", "-340", "-341", "-342", "-343", "-344", "-345", "-346", "-347", "-348", "-349", "-350", "-351", "-352", "-353", "-354", "-355", "-356", "-357", "-358", "-359", "-360", "-361", "-362", "-363", "-364", "-365", "-366", "-367", "-368", "-369", "-370", "-371", "-372", "-373", "-374", "-375", "-376", "-377", "-378", "-379", "-380", "-381", "-382", "-383", "-384", "-385", "-386", "-387", "-388", "-389", "-390", "-391", "-392", "-393", "-394", "-395", "-396", "-397", "-398", "-399", "-400", "-401", "-402", "-403", "-404", "-405", "-406", "-407", "-408", "-409", "-410", "-411", "-412", "-413", "-414", "-415", "-416", "-417", "-418", "-419", "-420", "-421", "-422", "-423", "-424", "-425", "-426", "-427", "-428", "-429", "-430", "-431", "-432", "-433", "-434", "-435", "-436", "-437", "-438", "-439", "-440", "-441", "-442", "-443", "-444", "-445", "-446", "-447", "-448", "-449", "-450", "-451", "-452", "-453", "-454", "-455", "-456", "-457", "-458", "-459", "-460", "-461", "-462", "-463", "-464", "-465", "-466", "-467", "-468", "-469", "-470", "-471", "-472", "-473", "-474", "-475", "-476", "-477", "-478", "-479", "-480", "-481", "-482", "-483", "-484", "-485", "-486", "-487", "-488", "-489", "-490", "-491", "-492", "-493", "-494", "-495", "-496", "-497", "-498", "-499", "-500", "-501", "-502", "-503", "-504", "-505", "-506", "-507", "-508", "-509", "-510", "-511", "-512", "-513", "-514", "-515", "-516", "-517", "-518", "-519", "-520", "-521", "-522", "-523", "-524", "-525", "-526", "-527", "-528", "-529", "-530", "-531", "-532", "-533", "-534", "-535", "-536", "-537", "-538", "-539", "-540", "-541", "-542", "-543", "-544", "-545", "-546", "-547", "-548", "-549", "-550", "-551", "-552", "-553", "-554", "-555", "-556", "-557", "-558", "-559", "-560", "-561", "-562", "-563", "-564", "-565", "-566", "-567", "-568", "-569", "-570", "-571", "-572", "-573", "-574", "-575", "-576", "-577", "-578", "-579", "-580", "-581", "-582", "-583", "-584", "-585", "-586", "-587", "-588", "-589", "-590", "-591", "-592", "-593", "-594", "-595", "-596", "-597", "-598", "-599", "-600", "-601", "-602", "-603", "-604", "-605", "-606", "-607", "-608", "-609", "-610", "-611", "-612", "-613", "-614", "-615", "-616", "-617", "-618", "-619", "-620", "-621", "-622", "-623", "-624", "-625", "-626", "-627", "-628", "-629", "-630", "-631", "-632", "-633", "-634", "-635", "-636", "-637", "-638", "-639", "-640", "-641", "-642", "-643", "-644", "-645", "-646", "-647", "-648", "-649", "-650", "-651", "-652", "-653", "-654", "-655", "-656", "-657", "-658", "-659", "-660", "-661", "-662", "-663", "-664", "-665", "-666", "-667", "-668", "-669", "-670", "-671", "-672", "-673", "-674", "-675", "-676", "-677", "-678", "-679", "-680", "-681", "-682", "-683", "-684", "-685", "-686", "-687", "-688", "-689", "-690", "-691", "-692", "-693", "-694", "-695", "-696", "-697", "-698", "-699", "-700", "-701", "-702", "-703", "-704", "-705", "-706", "-707", "-708", "-709", "-710", "-711", "-712", "-713", "-714", "-715", "-716", "-717", "-718", "-719", "-720", "-721", "-722", "-723", "-724", "-725", "-726", "-727", "-728", "-729", "-730", "-731", "-732", "-733", "-734", "-735", "-736", "-737", "-738", "-739", "-740", "-741", "-742", "-743", "-744", "-745", "-746", "-747", "-748", "-749", "-750", "-751", "-752", "-753", "-754", "-755", "-756", "-757", "-758", "-759", "-760", "-761", "-762", "-763", "-764", "-765", "-766", "-767", "-768", "-769", "-770", "-771", "-772", "-773", "-774", "-775", "-776", "-777", "-778", "-779", "-780", "-781", "-782", "-783", "-784", "-785", "-786", "-787", "-788", "-789", "-790", "-791", "-792", "-793", "-794", "-795", "-796", "-797", "-798", "-799", "-800", "-801", "-802", "-803", "-804", "-805", "-806", "-807", "-808", "-809", "-810", "-811", "-812", "-813", "-814", "-815", "-816", "-817", "-818", "-819", "-820", "-821", "-822", "-823", "-824", "-825", "-826", "-827", "-828", "-829", "-830", "-831", "-832", "-833", "-834", "-835", "-836", "-837", "-838", "-839", "-840", "-841", "-842", "-843", "-844", "-845", "-846", "-847", "-848", "-849", "-850", "-851", "-852", "-853", "-854", "-855", "-856", "-857", "-858", "-859", "-860", "-861", "-862", "-863", "-864", "-865", "-866", "-867", "-868", "-869", "-870", "-871", "-872", "-873", "-874", "-875", "-876", "-877", "-878", "-879", "-880", "-881", "-882", "-883", "-884", "-885", "-886", "-887", "-888", "-889", "-890", "-891", "-892", "-893", "-894", "-895", "-896", "-897", "-898", "-899", "-900", "-901", "-902", "-903", "-904", "-905", "-906", "-907", "-908", "-909", "-910", "-911", "-912", "-913", "-914", "-915", "-916", "-917", "-918", "-919", "-920", "-921", "-922", "-923", "-924", "-925", "-926", "-927", "-928", "-929", "-930", "-931", "-932", "-933", "-934", "-935", "-936", "-937", "-938", "-939", "-940", "-941", "-942", "-943", "-944", "-945", "-946", "-947", "-948", "-949", "-950", "-951", "-952", "-953", "-954", "-955", "-956", "-957", "-958", "-959", "-960", "-961", "-962", "-963", "-964", "-965", "-966", "-967", "-968", "-969", "-970", "-971", "-972", "-973", "-974", "-975", "-976", "-977", "-978", "-979", "-980", "-981", "-982", "-983", "-984", "-985", "-986", "-987", "-988", "-989", "-990", "-991", "-992", "-993", "-994", "-995", "-996", "-997", "-998", "-999" };


        /* ************************************ */
        /*                 IAM                  */
        /* ************************************ */
        /* ------------------------------------ */
        //  nature_of_connection_indicators
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator1(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator1";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator2(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator2";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator3(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator3";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator4(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator4";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator5(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator5";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator6(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator6";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator7(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator7";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator8(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator8";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator9(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator9";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator10(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator10";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator11(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite", p, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_", p, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator=>nonvalid", p, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator=>nonvalid=>", p, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator11";
        }
        public static string nature_of_connection_indicators__satellite_indicator12(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters_numbers, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator12";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator13(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters_numbers, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator13";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator14(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters_numbers, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator14";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__satellite_indicator15(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters_numbers, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", letters, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__satellite_indicator15";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator1(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator1";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator2(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator2";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator3(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator3";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator4(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator4";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator5(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator5";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator6(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator6";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator7(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator7";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator8(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", n, 2); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator8";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator9(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator9";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator10(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", null, 0); //INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator10";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator11(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", letters, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", letters, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator11";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__continuity_check_indicator12(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", letters_numbers, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", letters_numbers, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__continuity_check_indicator12";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator1(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator1";
        }
        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator2(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator2";
        }
        public static string nature_of_connection_indicators__echo_control_device_indicator3(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", empty, 0);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", n, 1);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", n, 1);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", empty, 0);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator3";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator4(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", empty, 0);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", n, 3);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", n, 3);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", empty, 0);//INVALID
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator4";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator5(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", empty, 0); //INVALID, se asigna una cadena vacía
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator5";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator6(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", null, 0); //INVALID, debe ser "null" sin el símbolo @
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", null, 0); //INVALID, debe ser "null" sin el símbolo @
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator6";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator7(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", null, 0); //INVALID, se debe proporcionar un valor válido
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", null, 0); //INVALID, se debe proporcionar un valor válido
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator7";
        }

        /* ------------------------------------ */
        public static string nature_of_connection_indicators__echo_control_device_indicator8(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>satellite_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>continuity_check_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", letters_numbers, 0); //INVALID, se debe proporcionar un valor válido
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "iam=>nature_of_connection_indicators=>echo_control_device_indicator", letters, 0); //INVALID, se debe proporcionar un valor válido
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>national_international_call_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_user_part_preference_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>sccp_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>ported_number_translation_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>forward_call_indicators=>query_on_release_attempt_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>calling_party_category=>calling_party_category", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>transmission_medium_requirement=>transmission_medium_requirement", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>nature_of_address_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>numbering_plan_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>internal_network_number_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "iam=>called_party_number=>address_signals", p, 0);
            return "nature_of_connection_indicators__echo_control_device_indicator8";
        }


        /* ************************************ */
        /*                 ACM                  */
        /* ************************************ */
        /* ------------------------------------ */
        //  backward_call__indicators
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator0(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call_indicators__charge_indicator0";
        }

        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator1(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator1";
        }
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator2(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call_indicators__charge_indicator2";
        }

        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator3(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator3";
        }
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator4(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator4";
        }
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator5(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 1);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator5";
        }
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator6(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 3); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 2);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator6";
        }
        /* ------------------------------------ */
        public static string backward_call_indicators__charge_indicator7(_isupss7_tree tree)
        {
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", p, 3);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", n, 1); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>charge_indicator", empty, 0); //INVALID
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_status_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>called_partys_category_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_method_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>interworking_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>end_to_end_information_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_user_part_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>holding_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>isdn_access_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>echo_control_device_indicator", p, 0);
            set_value_tree_isupss7lib(tree, "acm=>backward_call_indicators=>sccp_method_indicator", p, 0);
            return "backward_call__charge_indicator7";
        }
        /* ------------------------------------ */


        static _test[] tests = new _test[]
        {
            new _test { f = nature_of_connection_indicators__satellite_indicator1, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator2, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x01,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator3, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator4, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x03,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator5, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator6, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x01,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator7, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator8, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x03,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator9, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator9, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator10, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator11, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x01,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator12, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator13, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x01,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator14, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x02,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__satellite_indicator15, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x03,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator1, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator2, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x04,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator3, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x08,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator4, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x0C,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator5, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator6, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x04,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator7, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x08,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator8, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x0C,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator9, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x08,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator10, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x08,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator11, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__continuity_check_indicator12, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x04,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator1, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator2, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x10,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator3, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator4, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x10,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator5, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator6, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x10,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator7, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = nature_of_connection_indicators__echo_control_device_indicator8, message_type_in = isupss7_message_type.ISUPSS7_IAM, variant_in = isupss7_variant.ITU, size_in = 12, mybuf_in = new List<byte> {0x01,0x10,0x00,0x00,0x00,0x00,0x02,0x00,0x03,0x80,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator0, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x00,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator1, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x01,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator2, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x02,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator3, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x03,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator4, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x00,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator5, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x01,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator6, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x02,0x00,0x00}},
            new _test { f = backward_call_indicators__charge_indicator7, message_type_in = isupss7_message_type.ISUPSS7_ACM, variant_in = isupss7_variant.ANSI, size_in = 4, mybuf_in = new List<byte> {0x06,0x03,0x00,0x00}},
            new _test{f = null, message_type_in = 0, variant_in = 0, size_in = DefineConstants.NONSET, mybuf_in = new List<byte>()}
    };

        public static int calculate_len(_test[] tests)
        {
            int i = 0;
            while (i < tests.Length && tests[i].size_in != DefineConstants.NONSET)
            {
                i++;
            }
            return i;
        }


        internal static int Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tree = default(_isupss7_tree);
            isupss7_message_type message_type_in;
            isupss7_variant variant_in;
            int size_in;
            List<byte> mybuf_in;
            string test_name = "";
            int size_out = 0;
            byte[] mybuf_out = null;
            int len = calculate_len(tests);
            int success = 0;
            int fail = 0;

            for (var j = 0; j < len; j++)
            {
                message_type_in = tests[j].message_type_in;
                variant_in = tests[j].variant_in;
                size_in = tests[j].size_in;
                mybuf_in = tests[j].mybuf_in;

                Console.WriteLine("________________________________________________________________\n{0} - {1} - {2}", print_message_type(message_type_in), print_variant(variant_in), test_name);

                if (create_tree_isupss7lib(ref tree, variant_in, message_type_in) > 0)
                {
                    test_name = tests[j].f(tree);
                    //size_out = size_in;
                    encode_tree_isupss7lib(ref mybuf_out, ref size_out, ref tree);

                    for (var i = 0; i < size_in; i++)
                    {
                        Console.Write("{0:x2} ", mybuf_in[i]);
                    }
                    Console.WriteLine();

                    for (var i = 0; i < size_out; i++)
                    {
                        Console.Write("{0:x2} ", mybuf_out[i]);
                    }
                    Console.WriteLine();

                    if (idem_buffers(mybuf_in.ToArray(), size_in, mybuf_out, size_out) != 0)
                    {
                        success++;
                        Console.Write("result: \"ok\"\n");
                    }
                    else
                    {
                        fail++;
                        Console.WriteLine(" * Error in {0}.", test_name);
                    }

                    // ...
                    //dump_tree_isupss7lib(ref tree);

                    // No hay un equivalente directo de `free` en C#, la recolección de basura se maneja automáticamente
                    // free(mybuf_out);
                    destroy_tree_isupss7lib(tree);
                }
                else
                {
                    Console.WriteLine("There was an error while decoding");
                }
            }

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("total:{0}           success:{1}       fail:{2}", len, success, fail);
            Console.WriteLine("--------------------------------------------");

            stopwatch.Stop();
            var runTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("Run time is {0} seconds", runTime);

            var ret = success == fail ? 1 : 0;
            return ret;
        }

        public static int dump_tree_isupss7lib(ref _isupss7_tree tree)
        {
            isupss7_variant variant = tree.variant;
            isupss7_message_type message_type = (isupss7_message_type)tree.s_message_type.p_message_type.Value;

            switch (variant)
            {
                case isupss7_variant.ITU:
                    attach("Variant: ITU (Q.761 - Q.764, 1992)");
                    break;
                case isupss7_variant.ANSI:
                    attach("Variant: ANSI (T1.113, 1992)");
                    break;
            }

            attach("\n");
            dump_message_type();
            attach_separator_2();
            return 1;

        error:
            return -1;
        }

        public static int dump_message_type()
        {
            attach_separator();
            attach_title("message_type");
            attach("\n");
            return 1;
        }


    }
}
