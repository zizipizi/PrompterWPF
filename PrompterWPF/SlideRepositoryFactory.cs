using PrompterWPF.Interfaces;
using PrompterWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrompterWPF
{
    public class SlideRepositoryFactory
    {
        public ISlideRepository GetRepository(bool isManual)
        {
            var m = new ManualSliderepository();
            var a = new SlidesRepository();
            if (isManual) return m;
            return a;
        }
    }
}
