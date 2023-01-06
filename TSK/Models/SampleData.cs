using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSK.Models {

    static class SampleData {
        public static List<SampleOrder> Orders = new List<SampleOrder>() {
            new SampleOrder {
                ID = 1,
                Head_ID = -1,
                Full_Name = "John Heart",
                Prefix = "Mr.",
                Title = "CEO",
                City = "Los Angeles",
                State = "California",
                Email = "jheart@dx-email.com",
                Skype =  "jheart_DX_skype",
                Mobile_Phone =  "(213) 555-9392",
                Birth_Date =  "1964-03-16",
                Hire_Date = "1995-01-15"
                
            },
            new SampleOrder {
                ID= 2,
                Head_ID= 1,
                Full_Name= "Samantha Bright",
                Prefix= "Dr.",
                Title= "COO",
                City= "Los Angeles",
                State= "California",
                Email= "samanthab@dx-email.com",
                Skype= "samanthab_DX_skype",
                Mobile_Phone= "(213) 555-2858",
                Birth_Date= "1966-05-02",
                Hire_Date= "2004-05-24"
            },
            new SampleOrder {
                ID= 3,
                Head_ID= 1,
                Full_Name= "Arthur Miller",
                Prefix= "Mr.",
                Title= "CTO",
                City= "Denver",
                State= "Colorado",
                Email= "arthurm@dx-email.com",
                Skype= "arthurm_DX_skype",
                Mobile_Phone= "(310) 555-8583",
                Birth_Date= "1972-07-11",
                Hire_Date= "2007-12-18"
            },
            new SampleOrder {
                ID= 4,
                Head_ID= 1,
                Full_Name= "Robert Reagan",
                Prefix= "Mr.",
                Title= "CMO",
                City= "Bentonville",
                State= "Arkansas",
                Email= "robertr@dx-email.com",
                Skype= "robertr_DX_skype",
                Mobile_Phone= "(818) 555-2387",
                Birth_Date= "1974-09-07",
                Hire_Date= "2002-11-08"
            },
            new SampleOrder {
                ID= 5,
                Head_ID= 1,
                Full_Name= "Greta Sims",
                Prefix= "Ms.",
                Title= "HR Manager",
                City= "Atlanta",
                State= "Georgia",
                Email= "gretas@dx-email.com",
                Skype= "gretas_DX_skype",
                Mobile_Phone= "(818) 555-6546",
                Birth_Date= "1977-11-22",
                Hire_Date= "1998-04-23"
            },
            new SampleOrder {
                ID= 6,
                Head_ID= 3,
                Full_Name= "Brett Wade",
                Prefix= "Mr.",
                Title= "IT Manager",
                City= "Reno",
                State= "Nevada",
                Email= "brettw@dx-email.com",
                Skype= "brettw_DX_skype",
                Mobile_Phone= "(626) 555-0358",
                Birth_Date= "1968-12-01",
                Hire_Date= "2009-03-06"
            },
        };
    }
}
