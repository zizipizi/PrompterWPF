using PrompterWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrompterWPF.Interfaces
{
    public interface ISlideRepository
    {
        (IReadOnlyCollection<Slide> slides, int timer) GetSlides();
    }
}
