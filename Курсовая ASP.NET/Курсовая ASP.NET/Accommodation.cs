//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Курсовая_ASP.NET
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Accommodation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Date)]
        public System.DateTime SettlementDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DepartureDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int IdRoom { get; set; }
    
        public virtual Rooms Rooms { get; set; }
    }
}
