//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Курсовая_ASP.NET
{
    using System;
    
    public partial class AccommodationView_Result
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime SettlementDate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public int Number { get; set; }
    }
}
