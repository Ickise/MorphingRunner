// Cette interface me permet à des Scripts d'avoir la fonction UpdateTick() et de s'abonner à l'UpdateManager pour bénéficier de ces avantages.
public interface IUpdate
{
    void UpdateTick();
}