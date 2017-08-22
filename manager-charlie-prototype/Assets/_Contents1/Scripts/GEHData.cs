using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEHData{

    public static Dictionary<int, string> PhanixWords = new Dictionary<int, string>();

    //private static GEHData _instance;

    //public static GEHData Instance()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = new GEHData();
    //    }
    //    return _instance;
    //}

    public GEHData()
    { 
        PhanixWords.Clear();

        Debug.Log("haha");

        /* A */
        string Apple = "Apple";
        string Acorn = "Acorn";
        string Almond = "Almond";

        PhanixWords.Add(1, Apple);
        PhanixWords.Add(2, Acorn);
        PhanixWords.Add(3, Almond);

        /* B */
        string Bread = "Bread";
        string Bean = "Bean";
        string Banana = "Banana";

        PhanixWords.Add(4, Bread);
        PhanixWords.Add(5, Bean);
        PhanixWords.Add(6, Banana);

        /* C */
        string Cucumber = "Cucumber";
        string Carrot = "Carrot";
        string Cheese = "Cheese";

        PhanixWords.Add(7, Cucumber);
        PhanixWords.Add(8, Carrot);
        PhanixWords.Add(9, Cheese);

        /* D */
        string Daikon = "Daikon";
        string Date = "Date";
        string Dumpling = "Dumpling";

        PhanixWords.Add(10, Daikon);
        PhanixWords.Add(11, Date);
        PhanixWords.Add(12, Dumpling);

        /* E */
        string ElderBerry = "Elder Berry";
        string Egg = "Egg";
        string Eggplant = "Eggplant";

        PhanixWords.Add(13, ElderBerry);
        PhanixWords.Add(14, Egg);
        PhanixWords.Add(15, Eggplant);

        /* F */
        string Fish = "Fish";
        string Fig = "Fig";
        string Fennel = "Fennel";

        PhanixWords.Add(16, Fish);
        PhanixWords.Add(17, Fig);
        PhanixWords.Add(18, Fennel);

        /* G */
        string Grapes = "Grapes";
        string Garlic = "Garlic";
        string GreenPepper = "Green Pepper";

        PhanixWords.Add(19, Grapes);
        PhanixWords.Add(20, Garlic);
        PhanixWords.Add(21, GreenPepper);

        /* H */
        string Honey = "Honey";
        string HoneyDew = "Honey Dew";
        string Herbs = "Herbs";

        PhanixWords.Add(22, Honey);
        PhanixWords.Add(23, HoneyDew);
        PhanixWords.Add(24, Herbs);

        /* I */
        string IceCream = "Ice Cream";
        string IndianFood = "Indian Food";
        string ItalianFood = "Italian Food";
        string IceJelly = "Ice Jelly";
        string Iceberglectture = "iceberg lectture";
        string IndianCorn = "indian corn";

        PhanixWords.Add(25, IceCream);
        PhanixWords.Add(26, IndianFood);
        PhanixWords.Add(27, ItalianFood);
        PhanixWords.Add(28, IceJelly);
        PhanixWords.Add(29, Iceberglectture);
        PhanixWords.Add(30, IndianCorn);

        /* J */
        string Jam = "Jam";

        PhanixWords.Add(31, Jam);

        /* K */
        string Kimchi = "Kimchi";
        string kiwi = "kiwi";
        string kale = "kale";
        string kindeybean = "kindey bean";

        PhanixWords.Add(32, Kimchi);
        PhanixWords.Add(33, kiwi);
        PhanixWords.Add(34, kale);
        PhanixWords.Add(35, kindeybean);

        /* L */
        string Lemon = "Lemon";
        string Lamb = "Lamb";
        string Lentils = "Lentils";
        string Lettuce = "Lettuce";
        string Lime = "Lime";

        PhanixWords.Add(36, Lemon);
        PhanixWords.Add(37, Lamb);
        PhanixWords.Add(38, Lentils);
        PhanixWords.Add(39, Lettuce);
        PhanixWords.Add(40, Lime);

        /* M */
        string Macadamia = "Macadamia";
        string Mackerel = "Mackerel";
        string Mango = "Mango";
        string Mandarins = "Mandarins";
        string MapleSyrup = "Maple Syrup";

        PhanixWords.Add(41, Macadamia);
        PhanixWords.Add(42, Mackerel);
        PhanixWords.Add(43, Mango);
        PhanixWords.Add(44, Mandarins);
        PhanixWords.Add(45, MapleSyrup);

        /* N */
        string Nectarine = "Nectarine";
        string Nuts = "Nuts";
        string Noodles = "Noodles";
        string Nacho = "Nacho";
        string Nuggets = "Nuggets";

        PhanixWords.Add(46, Nectarine);
        PhanixWords.Add(47, Nuts);
        PhanixWords.Add(48, Noodles);
        PhanixWords.Add(49, Nacho);
        PhanixWords.Add(50, Nuggets);

        /* O */
        string Oatmeal = "Oatmeal";
        string Olives = "Olives";
        string Onion = "Onion";
        string Orange = "Orange";
        string Oysters = "Oysters";

        PhanixWords.Add(51, Oatmeal);
        PhanixWords.Add(52, Olives);
        PhanixWords.Add(53, Onion);
        PhanixWords.Add(54, Orange);
        PhanixWords.Add(55, Oysters);

        /* P */
        string Peach = "Peach";
        string Potato = "Potato";
        string Pumpkin = "Pumpkin";
        string Pears = "Pears";
        string Pepprmint = "Pepprmint";

        PhanixWords.Add(56, Peach);
        PhanixWords.Add(57, Potato);
        PhanixWords.Add(58, Pumpkin);
        PhanixWords.Add(59, Pears);
        PhanixWords.Add(60, Pepprmint);

        /* Q */
        string QuailEggs = "Quail Eggs";
        string Questadilla = "Questadilla";
        string Quinoa = "Quinoa";

        PhanixWords.Add(61, QuailEggs);
        PhanixWords.Add(62, Questadilla);
        PhanixWords.Add(63, Quinoa);

        /* R */
        string Raisins = "Raisins";
        string Rice = "Rice";
        string Ribs = "Ribs";
        string Rasberry = "Rasberry";
        string Rosemary = "Rosemary";
        string Ramen = "Ramen";
        string Ricotta = "Ricotta";

        PhanixWords.Add(64, Raisins);
        PhanixWords.Add(65, Rice);
        PhanixWords.Add(66, Ribs);
        PhanixWords.Add(67, Rasberry);
        PhanixWords.Add(68, Rosemary);
        PhanixWords.Add(69, Ramen);
        PhanixWords.Add(70, Ricotta);

        /* S */
        string Spaghetti = "Spaghetti";
        string Salad = "Salad";
        string Shirimp = "Shirimp";
        string Salmon = "Salmon";
        string Strawberries = "Strawberries";
        string Steak = "Steak";
        string Soup = "Soup";
        string Sausage = "Sausage";

        PhanixWords.Add(71, Spaghetti);
        PhanixWords.Add(72, Salad);
        PhanixWords.Add(73, Shirimp);
        PhanixWords.Add(74, Salmon);
        PhanixWords.Add(75, Strawberries);
        PhanixWords.Add(76, Steak);
        PhanixWords.Add(77, Soup);
        PhanixWords.Add(78, Sausage);

        /* T */
        string Toast = "Toast";
        string Turkey = "Turkey";
        string Tomato = "Tomato";
        string Turnip = "Turnip";
        string Tangerines = "Tangerines";
        string Tuna = "Tuna";
        string Trout = "Trout";

        PhanixWords.Add(79, Toast);
        PhanixWords.Add(80, Turkey);
        PhanixWords.Add(81, Tomato);
        PhanixWords.Add(82, Turnip);
        PhanixWords.Add(83, Tangerines);
        PhanixWords.Add(84, Tuna);
        PhanixWords.Add(85, Trout);

        /* U */
        string Udon = "Udon";

        PhanixWords.Add(86, Udon);

        /* V */
        string Vinegar = "Vinegar";
        string Veal = "Veal";
        string Vanilla = "Vanilla";

        PhanixWords.Add(87, Vinegar);
        PhanixWords.Add(88, Veal);
        PhanixWords.Add(89, Vanilla);

        /* W */
        string Walnut = "Walnut";
        string Waffles = "Waffles";
        string Wholemeal = "Wholemeal";
        string Whelk = "Whelk";
        string Wasabi = "Wasabi";
        string Watermelon = "Watermelon";

        PhanixWords.Add(90, Walnut);
        PhanixWords.Add(91, Waffles);
        PhanixWords.Add(92, Wholemeal);
        PhanixWords.Add(93, Whelk);
        PhanixWords.Add(94, Wasabi);
        PhanixWords.Add(95, Watermelon);

        /* Y */
        string Yam = "Yam";
        string Yeast = "Yeast";
        string Yoghurt = "Yoghurt";

        PhanixWords.Add(96, Yam);
        PhanixWords.Add(97, Yeast);
        PhanixWords.Add(98, Yoghurt);

        /* Z */
        string Zest = "Zest";
        string Zucchini = "Zucchini";

        PhanixWords.Add(99, Zest);
        PhanixWords.Add(100, Zucchini);
    }
}