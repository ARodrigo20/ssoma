using System.Collections.Generic;

namespace Hsec.Application.General.Login.Queries.GetLogin
{
    public class INavData
    {        
        public string name { get; set; }
        public string url { get; set; }           
        public string icon { get; set; }      
        public INavBadge badge { get; set; }
        public ICollection<INavData> children { get; set; }

        public INavData()
        {
            children = new HashSet<INavData>();
        }
        //public string variant { get; set; }
    }
    public class INavBadge
{
        public string variant { get; set; }
        public string text { get; set; }

        public INavBadge(string v, string t) {
            variant = v;
            text = t;
        }
    }
}
