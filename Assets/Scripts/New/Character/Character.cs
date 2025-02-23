public abstract class Character
{
    // J'ai créé une classe abstraite Character dont héritent les classes TRexCharacter, AlienCharacter et HumanCharacter.
    // Cela permet de centraliser les caractéristiques communes à tous les personnages.
    public abstract float Speed { get; }
    public abstract void SpecialAbility();
}