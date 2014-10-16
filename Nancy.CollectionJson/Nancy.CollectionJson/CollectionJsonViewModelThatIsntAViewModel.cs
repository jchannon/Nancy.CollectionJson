using System;
using System.IO;
using System.Collections.Generic;
using Nancy.Responses;
using Nancy.Json;
using System.Dynamic;
using CollectionJson;

namespace Nancy.CollectionJson
{

    public class CollectionJsonViewModelThatIsntAViewModel
    {
        public dynamic Model { get; set; }

        public Collection Links { get; set; }

        public CollectionJsonViewModelThatIsntAViewModel()
        {
            Model = new ExpandoObject();
            Links = new Collection();
        }
       
    }
}
