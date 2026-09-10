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

    public bool Attack(Monsters monsterBeingAttacked)
    {
        int damage_dealing = GetDamage();
        Console.WriteLine($"You suddenly, forcfully, with no respect of the well being of the {monsterBeingAttacked.Name}, \n attack it with a strength that in die terms is equivalent to {damage_dealing}.");
        monsterBeingAttacked.Health -= damage_dealing;
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
        return Game.DnDice();
    }
    public bool Attack(Character characterBeingAttacked)
    {
        if (Game.DnDice() > 3)
        {   
            Console.WriteLine($"The monster hit you for{Damage}!");
            return true;
        }
        else
        {
            Console.WriteLine("The monster attempted to attack you, but it missed.");
            return false;
        }
    }
}