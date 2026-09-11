namespace TextAdventure2;
public class Character
{
    public string Name;
    public int Health = 50;
    public List<string> Inventory = new List<string>();
    public string Location = "StartingArea";

    public int GetCharacterDamage()
    {
        int damage = 1; //fist idk
        if (Inventory.Contains("knife"))
        {
            damage += 5;
        }
        else if (Inventory.Contains("wood sword"))
        {
            damage += 2;
        }
        else if (Inventory.Contains("shining sword"))
        {
            damage += 7;
        }
        if (Inventory.Contains("charge blade"))
        {
            damage += 3;
        }
        if (Inventory.Contains("cactus"))
        {
            damage--;
        }
        return damage;
    }

    public void PrintInv()
    {
        Console.Write("Inventory contents:");
        if(Inventory.Count > 0) Console.Write(" | ");
        foreach (string item in Inventory)
        {
            Game.WriteColor(item, ConsoleColor.Blue);
            Console.Write(" | ");
        }
        
        Console.Write($"\n{Name} health: ");
        Game.WriteColor($"{Health}", ConsoleColor.Red);
        Console.Write("  Current damage: ");
        Game.WriteColor($"{GetCharacterDamage()}\n",ConsoleColor.DarkBlue);
    }

    public bool Attack(Monsters monsterBeingAttacked)
    {
        int damage_dealing = GetCharacterDamage();
        if (Game.DnDice() >= 5)
        {
        Console.WriteLine($"You suddenly, forcefully, with no respect or sympathy for the {monsterBeingAttacked.Name}, \n" +
                          $"attack it with a strength of {damage_dealing}.");
        }
        else
        {
            Console.WriteLine($"You attack with strength {damage_dealing}");
        }
        monsterBeingAttacked.Health -= damage_dealing;
        //Console.WriteLine($"{monsterBeingAttacked.Name} health is now {monsterBeingAttacked.Health}.");
        return true;
    }

    public void AddItemToInventory(string itemToAdd)
    {
        Inventory.Add(itemToAdd);
        Console.Write($"You picked up ");
        Game.WriteColor($"{itemToAdd}",ConsoleColor.Blue);
        Console.WriteLine(".");
    }
    public void RemoveItemFromInventory(string itemToRemove)
    {
        Inventory.Remove(itemToRemove);
        Console.Write($"You dropped or used the ");
        Game.WriteColor($"{itemToRemove}",ConsoleColor.Blue);
        Console.WriteLine(".");
    }
}
public class Monsters
{
    public int Health = 20;
    public string Name;
    public int Damage = 5;  

    public Monsters(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
    public int GetMonsterDamage()
    {
        return Damage;
    }
    public bool Attack(Character characterBeingAttacked)
    {
        if (Game.DnDice() > 1)
        {   
            Console.WriteLine($"The {Name} hit you for {Damage}!");
            characterBeingAttacked.Health -= GetMonsterDamage();
            return true;
        }
        else
        {
            Console.WriteLine($"The {Name} attempted to attack you, but it missed.");
            return false;
        }
    }
}