namespace Player.Weapons
{
    public class Rifle : Weapon
    {
        protected override void OnFire()
        {
            CreateAndSetBullet(_firePoint);
            base.OnFire();
        }
    }

}