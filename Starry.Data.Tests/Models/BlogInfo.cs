using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starry.Data.Tests.Models
{
    public class BlogInfo
    {
        public int BIID { set; get; }
        public string BITitle { set; get; }
        public string BIContent { set; get; }
        public int BICreateUser { set; get; }
        public DateTime BICreateTime { set; get; }
    }
}
