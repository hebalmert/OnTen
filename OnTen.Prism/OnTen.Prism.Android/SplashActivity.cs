using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;

namespace OnTen.Prism.Droid
{
    //[Activity(Theme = "@style/MainTheme.Splash",
    //          MainLauncher = true,
    //          NoHistory = true)]
    //public class SplashActivity : AppCompatActivity
    //{
    //    // Launches the startup task
    //    protected override void OnResume()
    //    {
    //        base.OnResume();
    //        StartActivity(new Intent(Application.Context, typeof(MainActivity)));
    //    }
    //}

    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(1800);
            StartActivity(typeof(MainActivity));
        }
    }
}
