using PrompterWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrompterWPF.Model
{
    public class ManualSliderepository : ISlideRepository
    {
        public (IReadOnlyCollection<Slide> slides, int timer) GetSlides()
        {
            var slides = new List<Slide>();
            slides.Add(new Slide { Label = "Loh", Number = 1, Texts = new List<string>() });
            slides.Add(new Slide { Label = "Loh2", Number = 2, Texts = new List<string>() });
            slides.Add(new Slide { Label = "Loh3", Number = 3, Texts = new List<string>() });

            return (slides, 10);
        }
    }
}
