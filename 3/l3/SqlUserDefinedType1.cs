
using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;



[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]
public class LicDescription : INullable, IBinarySerialize
{
    private bool _isNull;
    private string _soft;
    private string _price;

    public bool IsNull
    {
        get { return _isNull; }
    }

    public string SoftwareID
    {
        get { return _soft; }
        set { _soft = value; }
    }

    public string Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public static LicDescription Parse(SqlString s)
    {
        if (s.IsNull)
        {
            return Null;
        }

        string[] param = s.Value.Split(',');
        LicDescription u = new LicDescription();
        u._soft = param[0];
        u._price = param[1];
        return u;
    }

    public override string ToString()
    {
        if (this._isNull)
        {
            return "NULL";
        }

        return $"SoftwareID: {this._soft}, Price: {this._price}";
    }

    public static LicDescription Null
    {
        get
        {
            LicDescription h = new LicDescription();
            h._isNull = true;
            return h;
        }
    }

    public void Read(BinaryReader r)
    {
        SoftwareID = r.ReadString();
        Price = r.ReadString();
    }

    public void Write(BinaryWriter w)
    {
        w.Write(SoftwareID);
        w.Write(Price);
    }
}
