namespace SunamoClipboard._public.SunamoInterfaces.Interfaces;


public interface IClipboardHelperBase<String, ListString, Bool>
{
    String GetText();
    ListString GetLines();
    Bool ContainsText();
    void SetText(string s);
    void SetText2(string s);
    
    
    
    
    void SetText3(string s);
    void SetList(List<string> d);
    void SetLines(List<string> lines);
    void CutFiles(params string[] selected);
    
    
    void SetText(StringBuilder stringBuilder);
}