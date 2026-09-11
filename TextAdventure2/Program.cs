using System;
using System.Collections.Generic;

namespace TextAdventure2;

class Program
{
    static void Main(string[] args)
    {
        Game GameInstance = new Game();
        GameInstance.GameStart();
    }
}

public class Game{
    Character character = new Character();
    string GetAnswer()
    {
        while (true)
        {
            string response = Console.ReadLine().Trim().ToLower();
            if (response == "")
            {
                continue;
            }
            else if (response == "quit") //NOT IMPLEMENTED
            {
                throw new NotImplementedException("");
                int[] arr = new int[1];
                arr[1] = 0;
            }
            else
            {
                return response;
            }
        }
    }
    public void PressToContinue()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();
    }
    string AskChoise(string[] choises)
    {
        while (true)
        {
            Console.Write("You can choose one of the following: ");
            for (int i = 0; i < choises.Length; i++)
            {
                if (i != 0)
                {
                    Console.Write(", ");
                }

                Console.Write($"{i+1}: {choises[i]}");
            }

            Console.WriteLine(" ");
            string answer = GetAnswer();
            for (int i = 0; i < choises.Length; i++)
            {
                int.TryParse(answer, out int number); //number is 0 if parsing fails, valid options are 1 and above
                if (choises[i].ToLower() == answer || number == i+1)
                {
                    Console.WriteLine($"You choose {choises[i]}.");
                    return choises[i];
                }
            }
        }
    }
    bool AskYesOrNo()
    {
        string response = GetAnswer();
        if (response == "yes")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GameStart()
    {

        while (character.Location != "End")
        {
            if (character.Location == "StartingArea")
            {
                StartingArea();
            }
            else if (character.Location == "ancient forest")
            {
                AncientForest();
            }
            else if (character.Location == "Mountain Cave")
            {
                MountainCave();
            }
            else if (character.Location == "Deep Cave")
            {
                DeepCave();
            }
            else if (character.Location == "mountain_peak")
            {
                MountainPeak();
            }
            else if (character.Location == "Shrublands")
            {
                ShrubLands();
            }
            else if (character.Location == "Elders Recess")
            {
                EldersRecess();
            }else 
            {
                Console.Error.Write($"{character.Location} is not implemented!");
            }
        }
    }
    #region Rooms
    public void StartingArea()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Text Adventure!");
        do
        {
            Console.WriteLine("What is your name, adventurer");
            character.Name = GetAnswer();
            Console.WriteLine($"So {character.Name} is truly your name?");
        } while (!AskYesOrNo());

        Console.Clear();
        character.Location = "ancient forest";
    }

    public void AncientForest()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Ancient forest");
        character.AddItemToInventory("wooden sword");
        Console.WriteLine(
            "You are equipped with one wooden sword, and your task " +
            "is to slay the monster at the end of the adventure. " +
            "" +
            "In front of you is a stone table with two items on it, " +
            "a knife and a key." +
            "" +
            "You can only pick up one of these items."
        );
        string item_picked_upp = AskChoise(new string[] { "knife", "key" });
        character.AddItemToInventory(item_picked_upp);
        character.Location = "Mountain Cave";
        //Console.Clear();
    }

    public void MountainCave()
    {
        Console.WriteLine("Welcome to Mountain Cave Path!");
        if (character.Inventory.Contains("key"))
        {
            Console.WriteLine("Where do you want to go?");
            string choosen_place = AskChoise(new string[] { "deep cave", "mountain peak" });
            if (choosen_place == "deep cave")
            {
                character.Location = "Deep Cave";
            }
            else
            {
                character.Location = "mountain_peak";
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You can only go one way");
            character.Location = ("mountain_peak");
        }
    }

    public void DeepCave()
    {
        Console.Clear();
        Console.WriteLine("You see the keyhole in the wall.. You pick up the key from your pockets and place it in to the opening.\n" + 
                          "The ground trembles as you turn the key clockwise, your palms are sweaty arms heavy knees weak.\n"+ 
                          "The wall opens up to a deep path down into the deep cave, you slowly take the steps down into the darkness when you find\n"+
                          "a shining sword ");
        Console.WriteLine("Do you wish to pick up the shining sword and leave your current weapon");
        string weaponchoice = AskChoise(new string[] { "Pick up the sword", "Leave the sword and keep the wood sword." });
        if (weaponchoice == "Pick up the sword")
        {
            character.RemoveItemFromInventory("wood sword");
            character.AddItemToInventory("shining sword");
        }
        else
        {
            Console.WriteLine("So you dont want the shining sword??");
            Console.WriteLine($"This was your choice {character.Name}");
        }
        character.RemoveItemFromInventory("key");
        
        Console.WriteLine(
            "You see no way of going forward, so you make the conclusion of going back and picking the other path.\n" +
            "You head to the Mountain Peak");
        PressToContinue();
        character.Location = "mountain_peak";
    }

    public void MountainPeak()
    {
        Console.Clear();
        Console.WriteLine("The Mountain Peaks");
        Console.WriteLine("There is a dead Anjanath lying on the floor, its flesh made of dull gold.. rotted.\n" +
                          "You can clearly see something shiny in its hand, the shine is glistening in the sun\n" +
                          "Do approach the monster or will you walk away.");
        string approach = AskChoise(new string[] { "Approach", "Walk Away" });
        if (approach == "Approach")
        {
            int rolling = DnDice();
            if (rolling >= 2 && rolling <= 5)
            {
                Console.Clear();
                Console.WriteLine("You picked up the shiny item");
                Console.WriteLine("when you look closer at it you notice that it's just a cactus.\n" +
                                  "A cactus is a plant found in deserts and badlands. It grows over time and can sprout cactus flowers.\n" +
                                  "It damages mobs and destroys minecarts and dropped items that touch it.");
                character.AddItemToInventory("cactus"); // Bad item eller cursed item
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
            }
            else if (rolling > 5)
            {
                Console.Clear();
                Console.WriteLine("You just got a lot luckier than you think.\n" +
                                  "You have picked up a Charge Blade that was left beside the monster");
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You're hands are slippery and your attempt att picking upp the item unluckily fails");
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
            }
        }
        else
        {
            Console.WriteLine("So you see that it's perfectly safe loot, buy you chose to just ignore it??\n"+
                              "Come on what kind of devs are we if we just let you walk away from perfectly safe treasure.\n"+
                              "It's just a dead Anjanath, go ahead and take the glistening item no-one will see it.");
            string approach2 = AskChoise( new string[] { "Try Approaching the Anjanath", "Walk Away" });
            int rolling = DnDice();
            if (rolling >= 2 && rolling <= 5)
            {
                Console.Clear();
                Console.WriteLine("You picked up the shiny item");
                Console.WriteLine("when you look closer at it you notice that it's just a cactus.\n" +
                                  "A cactus is a plant found in deserts and badlands. It grows over time and can sprout cactus flowers.\n" +
                                  "It damages mobs and destroys minecarts and dropped items that touch it.");
                character.AddItemToInventory("cactus"); // Bad item eller cursed item
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
            }
            else if (rolling > 5)
            {
                Console.Clear();
                character.AddItemToInventory("charge blade");
                Console.WriteLine("You just got a lot luckier than you think.\n" +
                                  "You have picked up a Charge Blade that was left beside the monster");
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
               
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You're hands are slippery and your attempt att picking upp the item unluckily fails anyway so just leave...");
                Console.WriteLine("You move on..");
                character.Location = "Shrublands";
            }
        }
        PressToContinue();
    }

    public void ShrubLands()
    {
        Console.WriteLine("After the encounter with the dead Anjanath, you chose to continue your path and you find yourself in the Scrublands.");
        Console.WriteLine("While you traverse the scraggy shrubland,\n" +
                          "you are hit by occasional gusts of gray wind,\n" +
                          /*"Rarely they may carry a sliver of straw, sticking to your clothes for a brief moment\n"+
                          "before reuniting with the flow of the wind in your opposite direction."+*/
                          "stricken by the beauty of the landscape as it may have once been,\n" +
                          "increasingly annoyed by the decision of five pebbles and a sliver of straw massaging your feet.\n" +
                          "But you are not hurt, as the experience of the shrubland is not dangerous. Not yet...");
        PressToContinue();
        Console.WriteLine("Do you feel like tripping on a rock?");
        AskChoise(new string[]{"yes"});
        int dieRoll = DnDice();
        if (dieRoll == 6)
        {
            Console.WriteLine("You feel a sense of your knees and palms staring at you in disappointment.\n" +
                              "What are you trying to do? Offer yourself to the shrubs?\n" +
                              "They are not as round as the landscape when you see them from within.\n" +
                              "Your jurney will continue, but at the cost of 6 health points.");
            character.Health -= dieRoll;
        }
        else
        {
            Console.WriteLine($"A satisfactory tripping is experienced. Your physical health is degraded by a factor of {dieRoll}.");
            character.Health -= dieRoll;
        }
        PressToContinue();
        character.Location = "Elders Recess";
    }
    public void EldersRecess()
    {
        Console.Clear();
        Console.WriteLine("You decide to keep moving on. After walking for a while you find yourself in an a vulcanous area with ranges of elevated terrain.\n"+
                          "After exploring for a while you find yourself in a huge cave with an entrance for the sun to shine in, while looking around you also notice\n" + 
                          "a sharp glare from the suns vibrant rays bouncing of gigantic crystals. After wandering around you hear something\n"+
                          "or rather somethings presence in the cave. Then you notice it.. and it notices you... a large dragon, around 4 meters in height, and atleast 24 meters in lenght\n" +
                          "The dragon starts to circle around you, it walks slowly around you, holding an elegant stance almost like a Queen waiting for a moment to strike\n"+
                          "The dragon reveals itself and, its sleek, icy-blue body shimmering like frozen crystal, \n" +
                          "crowned by elegant swept-back horns and adorned with long, blade-like ice spines along its neck, wings, and tail.\n" +
                          $"It is time {character.Name} it's time to get monster hunting.");
                          PressToContinue();
                          Console.Clear();
        Monsters Velkhana = new Monsters("Velkhana", 100, 5);
        FightEvent(Velkhana);
    }
    #endregion Rooms

    public static int DnDice()
    {
        Random random = new Random();
        int roll = random.Next(1, 6);
        Console.WriteLine($"The dice rolls! {roll}"); 
        return roll;
    }

    public bool FightEvent(Monsters monster)
    {
        Console.WriteLine(
            $"Welcome to a fight event! \n you are fighting a {monster.Name} with {monster.Health} health.\n" +
            $"{monster.Name} is exited to hurt you with basedamage of {monster.Damage}.");
        while (monster.Health > 0) //rounds
        {
            Console.Clear();
            Console.WriteLine($"{character.Name} health: {character.Health}");
            Console.WriteLine($"{monster.Name} health: {monster.Health}");

            //player turn
            switch (AskChoise(new string[] { "attack", "run", "do a flip" }))
            {
                case "attack":
                    //characther.Attack(monster);
                    int damage_dealing = character.GetDamage();
                    Console.WriteLine(
                        $"You suddenly, forcefully, with no respect of the well being of the {monster.Name}, \n attack it with a strength that in die terms is equivalent to {damage_dealing}.");
                    monster.Hurt(damage_dealing);
                    Console.WriteLine($"monster health is now {monster.Health}.");
                    if (monster.Health <= 0)
                    {
                        Console.WriteLine("monster is unhealthy.");
                        return true;
                    }
                    break;
                case "run":
                    break;
                case "do a flip":
                    if (DnDice() == 6)
                    {
                        Console.WriteLine(
                            $"{monster.Name} thinks you look silly. It is dying of laughter. You have won the battle.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(
                            "You land head first on the ground. You feel the weight of your body concentrate at your neck, forming a small insignificant crack in your spine.\n" +
                            "Sound of thin metal colliding with ground is heard, it's your coffee thermos. You cannot live without coffee.\n"+
                            "One bit of emotional damage is taken.");
                        character.Hurt(1);
                    }
                    break;
                default:
                    break;
            }
            if (monster.Health <= 0)
            {
                Console.WriteLine($"You have killed {monster.Name}.");
                return true;
            }
            //monster turn
            int monsterDieRoll = DnDice();
            if (monsterDieRoll >= 3)
            {
                monster.Attack(character);
            }
            else
            {
                Console.WriteLine("monster does smt else than attack.");
            }
            if (character.Health <= 0)
            {
                Console.WriteLine("You died.");
                return false;
            }
            PressToContinue(); //detta är längst ned i while
        }
        return true; //cause error, need change
    }
}
    