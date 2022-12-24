using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Entities.Models.ViewModels
{
    public enum EnumOrderState
    {
        [Display(Name ="Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Tamamlandı")]
        Completed
    }
}
