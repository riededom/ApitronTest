using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using PDFExport;
using Environment = Android.OS.Environment;
using Path = System.IO.Path;

namespace ApitronTestApp1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public MainActivity()
        {
            pdf = new PdfExporter();
        }

        private PdfExporter pdf;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar =
            FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            CheckAppPermissions();
            //pdf = new PdfExporter();
            Button fab = FindViewById<Button>(Resource.Id.button);
            fab.Click += SaveDocument;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        private void SaveDocument(object sender, EventArgs e)
        {
            pdf.SaveDocument(GetFilePath());
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void CheckAppPermissions()
        {
            if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted ||
                PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
            {
                var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                RequestPermissions(permissions, 1);
            }
        }

        private string GetFilePath()
        {
            String path = Path.Combine(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath,
                "jjj.pdf");
            return path;
        }
    }
}

