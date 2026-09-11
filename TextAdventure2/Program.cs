using System;

namespace TextAdventure2;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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
            else if (response == "i" || response == "inventory")
            {
                character.PrintInv(); 
            }
            else
            {
                return response;
            }
        }
    }
    public void PressToContinue()
    {
        WriteColor("Press any key to continue...\n", ConsoleColor.DarkYellow);
        Console.ReadKey();
        Console.Clear();
    }
    string AskChoice(string[] choises)
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

                Console.Write($"{i+1}: ");
                WriteColor(choises[i], ConsoleColor.Yellow);
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
        if (response == "yes" || response == "ye" ||response == "y" || response == "1" || response == "hell yeah" || response == "fuck yeah" || response == "k" || response == "ok" || response == "go girl"|| response == "slay")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void WriteColor(string str, ConsoleColor col)
    {
        Console.ForegroundColor = col;
        Console.Write(str);
        Console.ResetColor();
    }
    public void GameStart()
    {
        while (character.Location != "End")
        {
            Console.Clear();
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
            else if (character.Location == "Elders Recess")
            {
                EldersRecess();
            }
            else if (character.Location == "Shrublands")
            {
                ShrubLands();
            }
            else if (character.Location == "Win")
            {
                Win();
            }
            else if (character.Location == "Lose")
            {
                Lose();
            }
            else if (character.Location == "Game Over")
            {
                GameOver();
            }
            else 
            {
                Console.Write($"{character.Location} is not implemented!");
                character.Location = "Lose";
            }
        }
    }
    #region Rooms
    public void StartingArea()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Text Adventure!");
        Console.WriteLine("Type your choice or its number to select it.\n"+
                          "You can also type \"i\" or \"inventory\" at almost any time to view your inventory.");
        do
        {
            Console.WriteLine("What is your name, adventurer");
            character.Name = GetAnswer();
            Console.WriteLine($"So {character.Name} is truly your name?");
        } while (!AskYesOrNo());

        character.Location = "ancient forest";
    }

    public void AncientForest()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Ancient forest");
        character.AddItemToInventory("wood sword");
        Console.WriteLine(
            "You are equipped with one wooden sword, and your task " +
            "is to slay the monster at the end of the adventure. " +
            "" +
            "In front of you is a stone table with two items on it, " +
            "a knife and a key." +
            "" +
            "You can only pick up one of these items."
        );
        string item_picked_upp = AskChoice(new string[] { "knife", "key" });
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
            string choosen_place = AskChoice(new string[] { "deep cave", "mountain peak" });
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
            Console.WriteLine("You can only go one way");
            character.Location = "mountain_peak";
            PressToContinue();
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
        string weaponchoice = AskChoice(new string[] { "Pick up the sword", "Leave the sword and keep the wood sword" });
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
        string approach = AskChoice(new string[] { "Approach", "Walk Away" });
        Console.Clear();
        if (approach == "Approach")
        {
            int rolling = DnDice();
            if (rolling >= 2 && rolling <= 5)
            {
                Console.WriteLine("You picked up the shiny item");
                Console.WriteLine("when you look closer at it you notice that it's just a cactus.\n" +
                                  "A cactus is a plant found in deserts and badlands. It grows over time and can sprout cactus flowers.\n" +
                                  "It damages mobs and destroys minecarts and dropped items that touch it.");
                character.AddItemToInventory("cactus"); // Bad item eller cursed item
                Console.WriteLine("You move on..");
            }
            else if (rolling > 5)
            {
                Console.WriteLine("You just got a lot luckier than you think.\n" +
                                  "You have picked up a Charge Blade that was left beside the Anjanath");
                character.AddItemToInventory("charge blade");
                Console.WriteLine("You move on..");
            }
            else
            {
                Console.WriteLine("Your hands are slippery and your attempt att picking upp the item unluckily fails");
                Console.WriteLine("You move on..");
            }
        }
        else
        {
            Console.WriteLine("So you see that it's perfectly safe loot, but you chose to just ignore it??\n"+
                              "Come on what kind of devs are we if we just let you walk away from perfectly safe treasure.\n"+
                              "It's just a dead Anjanath, go ahead and take the glistening item no-one will see it.");
            string approach2 = AskChoice( new string[] { "Try Approaching the Anjanath", "Walk Away" });
            if (approach2 == "Try Approaching the Anjanath")
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
                }
                else if (rolling == 6)
                {
                    Console.Clear();
                    character.AddItemToInventory("charge blade");
                    Console.WriteLine("You just got a lot luckier than you think.\n" +
                                      "You have picked up a Charge Blade that was left beside the monster");
                    Console.WriteLine("You move on..");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(
                        "You're hands are slippery and your attempt att picking upp the item unluckily fails anyway so just leave...");
                    Console.WriteLine("You move on..");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You walk away successfully");
                Console.WriteLine("There is a sheep a short distance away.\n"+
                                  "Do you want to fight it?");
                bool wantToFightSheep = AskYesOrNo();
                if (wantToFightSheep)
                {
                    Monsters Sheep = new Monsters("Sheep", 20, 1);
                    bool alive = FightEvent(Sheep);
                    if (!alive)
                    {
                        character.Location = "Lose";
                    }
                }
            }

        }

        if (character.Location == "mountain_peak") //to avoid overriding "lose" from sheep fight
        {
            character.Location = "Shrublands";
            Console.WriteLine("Walking to shrublands");
        }
        PressToContinue();
    }

    public void ShrubLands()
    {
        Console.WriteLine("After the encounter with the dead Anjanath, you chose to continue your path and you find yourself in the shrubland.");
        Console.WriteLine("While you traverse the scraggy shrubland,\n" +
                          "you are hit by occasional gusts of gray wind,\n" +
                          "bitten by mosquitoes who have not seen blood in decades,\n" +
                          "increasingly annoyed by five pebbles and a sliver of straw grinding your feet.\n" +
                          "But you are not hurt, as the experience of the shrubland is not dangerous. Not yet...");
        PressToContinue();
        Console.WriteLine("Do you feel like tripping on a rock?");
        string[] noResponses =
        {
            "But you do want to trip thought", "Yes", "Just say yes",
            "It wasn't actually a question, just show that you understand.",
            "Ok but the stones and shrubs wants you to. Will you do it for them?", "You are very bored, it could be fun ok?"
        };
        while (true)
        {
            if (!AskYesOrNo())
            {
                Console.WriteLine(noResponses[DnDice()]);
            }
            else
            {
                break;
            }
        }
        int dieRoll = DnDice();
        if (dieRoll <= 2)
        {
            Console.WriteLine("The ground seems sparse in rocks and you give up the search.\n" +
                              "Perhaps you may stumble upon a rock another time.");
        }
        else if (dieRoll == 6)
        {
            Console.WriteLine("You feel a sense of your knees and palms staring at you in disappointment.\n" +
                              "What are you trying to do? Offer yourself to the shrubs?\n" +
                              "They welcome you with open branches, pointy ones.\n" +
                              "Your journey will continue, but at the cost of 6 health points.");
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
        Monsters Velkhana = new Monsters("Velkhana", 80, 5);
        bool alive = FightEvent(Velkhana);
        if (alive)
        {
            character.Location = "Win";
        }
        else
        {
            character.Location = "Lose";
        }
    }

    public void Win()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You won {character.Name}.");
        PressToContinue();
        character.Location = "Game Over";
    }

    public void Lose()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"You lost {character.Name}.");
        PressToContinue();
        character.Location = "Game Over";
    }

    public void GameOver()
    {
        Console.WriteLine($"The Adventure is over. Do you wish to play again?");
        bool yesido = AskYesOrNo();
        if (yesido)
        {
            character = new Character(); //reset its variables
            character.Location = "StartingArea";
            return;
        }
        character.Location = "End";
    }

    #endregion Rooms

    public static int DnDice()
    {
        Random random = new Random();
        int roll = random.Next(1, 6);
        //WriteColor($"The dice rolls! {roll}",ConsoleColor.Gray); 
        return roll;
    }

    public bool FightEvent(Monsters monster)
    {
        Console.WriteLine(
            $"Welcome to a fight event! \nYou are fighting a {monster.Name} with {monster.Health} health.\n" +
            $"{monster.Name} is exited to hurt you with damage {monster.Damage}.");
        PressToContinue();
        while (monster.Health > 0) //rounds
        {
            Console.Clear();
            Console.WriteLine($"{character.Name} health stands at {character.Health}");
            Console.WriteLine($"{monster.Name} health stands at {monster.Health}");
            
            //player turn
            string[] fightActionChoises = new string[] { "attack", "run", "do a flip" };
            if (character.Inventory.Contains("cactus"))
            {
                fightActionChoises = new string[] { "attack", "run", "do a flip", "heal" };
            }
            switch (AskChoice(fightActionChoises))
            {
                case "attack":
                    character.Attack(monster);
                    break;
                case "run":
                    Console.WriteLine("You are running.");
                    break;
                case "do a flip":
                    if (DnDice() > 4)
                    {
                        Console.WriteLine(
                            $"{monster.Name} thinks you look silly. It is dying of laughter. You have won the battle.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(
                            "You land head first on the ground. You feel the weight of your body concentrate at your neck,\n" +
                            "forming a small crack in your spine. This is a silly way to die.");
                        PressToContinue();
                        return false;
                    }
                case "heal":
                    if (character.Inventory.Contains("cactus"))
                    {
                        character.Health += 15;
                        character.RemoveItemFromInventory("cactus");
                        Console.WriteLine("you healed 15.");
                    }
                    break;
                default:
                    break;
            }

            if (monster.Health <= 0)
            {
                Console.WriteLine($"You have slayed {monster.Name}.");
                PressToContinue();
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
                Console.WriteLine($"{monster.Name} prances and huffs at you");
            }
            if (character.Health <= 0)
            {
                Console.WriteLine("You died.");
                return false;
            }
            PressToContinue();
        }
        
        return true;
    }
}
    