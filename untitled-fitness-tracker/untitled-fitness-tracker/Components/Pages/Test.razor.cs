using System.Threading.Tasks;
using Components;
using Microsoft.AspNetCore.Components;
using UntitledFitnessTracker.Models;

namespace UntitledFitnessTracker.Components.Pages {

    public partial class Test
    {
        [Inject] public required AppDbContext DbContext {get; set;}

        public void TestButton()
        {
            
        }
    }
}