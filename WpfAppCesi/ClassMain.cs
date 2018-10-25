using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCesi
{
    class ClassMain
    {
        public ClassMain()
        {
            using (var db = new ModelBooking())
            {
                //var test = new HotelsSet()
                //{
                //    Nom = "Mercury",
                //    Capacite = 15,

                //};
                //db.HotelsSet.Add(test);
                db.SaveChanges();
            }
        }
    }
}
