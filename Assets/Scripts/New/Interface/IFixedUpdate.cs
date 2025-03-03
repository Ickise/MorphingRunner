// Cette interface me permet à des Scripts d'avoir la fonction FixedUpdateTick() et de s'abonner à l'UpdateManager pour bénéficier de ces avantages.
public interface IFixedUpdate
{
    void FixedUpdateTick();
}