namespace Player.Weapons
{
    public class Pistol : Weapon
    {
        protected override void OnFire()
        {
            CreateAndSetBullet(_firePoint);

            base.OnFire();
        }
    }

}