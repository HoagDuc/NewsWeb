using System.Text.RegularExpressions;

namespace NewsWeb.Extention
{
  public static class Extension
  {
    public static string ToVnd(this double price)
    {
      return price.ToString("#,##0") + " d";
    }

    public static string ToUrlFriendly(this string url)
    {
      var result = url.ToLower().Trim();
      result = Regex.Replace(result, "aàảãáạăằẳẵắặâầẩẫấậ", "a");
      result = Regex.Replace(result, "eèẻẽéẹêềểễếệ", "e");
      result = Regex.Replace(result, "oòỏõóọôồổỗốộơờởỡớợ", "o");
      result = Regex.Replace(result, "uùủũúụưừửữứự", "u");
      result = Regex.Replace(result, "iìỉĩíị", "i");
      result = Regex.Replace(result, "yỳỷỹýỵ", "y");
      result = Regex.Replace(result, "đ", "d");
      result = Regex.Replace(result, "[^a-z0-9-]", "");
      result = Regex.Replace(result, "(-)+", "-");


      return result;
    }
  }
}
