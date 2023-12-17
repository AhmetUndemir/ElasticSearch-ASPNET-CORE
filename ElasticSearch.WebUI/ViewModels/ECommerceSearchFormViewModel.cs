using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.WebUI.ViewModels;

public class ECommerceSearchFormViewModel
{
    [Display(Name = "Kategori")]
    public string? Category { get; set; }
    [Display(Name = "Cinsiyet")]
    public string? Gender { get; set; }
    [Display(Name = "Sipariş Başlangıç Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? OrderDateStart { get; set; }
    [Display(Name = "Sipariş Bitiş Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? OrderDateEnd { get; set; }
    [Display(Name = "Müşteri Adı & Soyadı")]
    public string? CustomerFullName { get; set; }
}
