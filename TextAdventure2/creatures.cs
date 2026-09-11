namespace TextAdventure2;
public class Character
{
    public string Name;
    public int Health = 100;
    public List<string> Inventory = new List<string>();
    public string Location = "StartingArea";

    public int GetDamage()
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
        return damage;
    }
    public void Hurt(int damage)
    {
        Health -= damage;
        Health = Math.Max(0, Health);
        Console.WriteLine($"{Name} health: {Health}");
        if (Health <= 0) { Die(); }
    }

    public void Die()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(("You Died"));
        Console.ResetColor();
        Location = "Debatable";
    }
    public bool Attack(Monsters monsterBeingAttacked)
    {
        int damage_dealing = GetDamage();
        Console.WriteLine($"You suddenly, forcfully, with no respect of the well being of the {monsterBeingAttacked.Name}, \n attack it with a strength that in die terms is equivalent to {damage_dealing}.");
        monsterBeingAttacked.Hurt(damage_dealing);
        Console.WriteLine($"monster health is now {monsterBeingAttacked.Health}.");
        return true;//not imp
    }

    public void AddItemToInventory(string itemToAdd)
    {
        Inventory.Add(itemToAdd);
        Console.WriteLine($"You picked up {itemToAdd}.");
    }
    public void RemoveItemFromInventory(string itemToRemove)
    {
        Inventory.Remove(itemToRemove);
        Console.WriteLine($"You dropped the {itemToRemove}");
    }
}
public class Monsters
{
    public int Health = 100;
    public string Name;
    public int Damage = 10;  

    public Monsters(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
    public int GetDamage()
    {
        return Damage;
    }
    public void Hurt(int damage)
    {
        Health -= damage;
        Health = Math.Max(0, Health);
        Console.WriteLine($"{Name} health: {Health}");
        if (Health <= 0) { Die(); }
    }

    public void Die()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(($"{Name} Died"));
        Console.ResetColor();
    }

    public bool Attack(Character characterBeingAttacked)
    {
        int dieRoll = Game.DnDice();
        int damage_dealing = GetDamage() + dieRoll == 6 ? 1 : 0;
        if (dieRoll > 3)
        {   
            Console.WriteLine($"The monster hit you for{damage_dealing}!");
            characterBeingAttacked.Hurt(damage_dealing);
            return true;
        }
        else
        {
            Console.WriteLine("The monster attempted to attack you, but it missed.");
            return false;
        }
    }
}