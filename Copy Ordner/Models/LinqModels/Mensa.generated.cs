//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace DataModel
{
	/// <summary>
	/// Database       : test
	/// Data Source    : localhost
	/// Server Version : 5.5.5-10.3.10-MariaDB
	/// </summary>
	public partial class Mensa : LinqToDB.Data.DataConnection
	{
		public ITable<Benutzer>                  Benutzer                  { get { return this.GetTable<Benutzer>(); } }
		public ITable<Benutzerxbenutzer>         Benutzerxbenutzer         { get { return this.GetTable<Benutzerxbenutzer>(); } }
		public ITable<Bestellungen>              Bestellungen              { get { return this.GetTable<Bestellungen>(); } }
		public ITable<Bilder>                    Bilder                    { get { return this.GetTable<Bilder>(); } }
		public ITable<Deklarationen>             Deklarationen             { get { return this.GetTable<Deklarationen>(); } }
		public ITable<Fachbereiche>              Fachbereiche              { get { return this.GetTable<Fachbereiche>(); } }
		public ITable<Fhangeh�rige>              Fhangeh�rige              { get { return this.GetTable<Fhangeh�rige>(); } }
		public ITable<Fhangeh�rigexfachbereiche> Fhangeh�rigexfachbereiche { get { return this.GetTable<Fhangeh�rigexfachbereiche>(); } }
		public ITable<G�ste>                     G�ste                     { get { return this.GetTable<G�ste>(); } }
		public ITable<Kategorien>                Kategorien                { get { return this.GetTable<Kategorien>(); } }
		public ITable<Kommentare>                Kommentare                { get { return this.GetTable<Kommentare>(); } }
		public ITable<Mahlzeiten>                Mahlzeiten                { get { return this.GetTable<Mahlzeiten>(); } }
		public ITable<Mahlzeitenxbestellungen>   Mahlzeitenxbestellungen   { get { return this.GetTable<Mahlzeitenxbestellungen>(); } }
		public ITable<Mahlzeitenxbilder>         Mahlzeitenxbilder         { get { return this.GetTable<Mahlzeitenxbilder>(); } }
		public ITable<Mahlzeitenxdeklarationen>  Mahlzeitenxdeklarationen  { get { return this.GetTable<Mahlzeitenxdeklarationen>(); } }
		public ITable<Mitarbeiter>               Mitarbeiter               { get { return this.GetTable<Mitarbeiter>(); } }
		public ITable<Preise>                    Preise                    { get { return this.GetTable<Preise>(); } }
		public ITable<Student>                   Student                   { get { return this.GetTable<Student>(); } }
		public ITable<Zutaten>                   Zutaten                   { get { return this.GetTable<Zutaten>(); } }
		public ITable<Zutatenxmahlzeiten>        Zutatenxmahlzeiten        { get { return this.GetTable<Zutatenxmahlzeiten>(); } }
		public ITable<Zutatjoin>                 Zutatjoin                 { get { return this.GetTable<Zutatjoin>(); } }

		public void InitMappingSchema()
		{
		}

		public Mensa()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public Mensa(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext();
	}

	[Table("benutzer")]
	public partial class Benutzer
	{
		[Column(),         PrimaryKey,  Identity] public uint      Nummer       { get; set; } // int(10) unsigned
		[Column("E-Mail"), NotNull              ] public string    EMail        { get; set; } // varchar(100)
		[Column(),            Nullable          ] public DateTime? LetzterLogin { get; set; } // datetime
		[Column(),         NotNull              ] public string    Nutzername   { get; set; } // varchar(30)
		[Column(),            Nullable          ] public DateTime? Geburtsdatum { get; set; } // date
		[Column(),         NotNull              ] public bool      Aktiv        { get; set; } // tinyint(1)
		[Column(),         NotNull              ] public DateTime  Anlegedatum  { get; set; } // date
		[Column(),            Nullable          ] public uint?     Alter        { get; set; } // int(10) unsigned
		[Column(),         NotNull              ] public string    Vorname      { get; set; } // varchar(50)
		[Column(),         NotNull              ] public string    Nachname     { get; set; } // varchar(25)
		[Column(),         NotNull              ] public string    Salt         { get; set; } // varchar(32)
		[Column(),         NotNull              ] public string    Hash         { get; set; } // varchar(24)
		[Column(),         NotNull              ] public string    ISA          { get; set; } // enum('Gast','FHAngeh�rige')

		#region Associations

		/// <summary>
		/// BenutzerDelFHA_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Fhangeh�rige BenutzerDelFHA { get; set; }

		/// <summary>
		/// BenutzerDelGast_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public G�ste BenutzerDelGast { get; set; }

		/// <summary>
		/// benutzerxbenutzer_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="Benutzerzwei", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Benutzerxbenutzer> BenutzerxbenutzerIbfk2BackReferences { get; set; }

		/// <summary>
		/// benutzerxbenutzer_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="Benutzereins", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Benutzerxbenutzer> Benutzerxbenutzeribfks { get; set; }

		/// <summary>
		/// bestellungen_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="Benutzer", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Bestellungen> Bestellungenibfks { get; set; }

		#endregion
	}

	[Table("benutzerxbenutzer")]
	public partial class Benutzerxbenutzer
	{
		[Column, Nullable] public uint? Benutzereins { get; set; } // int(10) unsigned
		[Column, Nullable] public uint? Benutzerzwei { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// benutzerxbenutzer_ibfk_2
		/// </summary>
		[Association(ThisKey="Benutzerzwei", OtherKey="Nummer", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="benutzerxbenutzer_ibfk_2", BackReferenceName="BenutzerxbenutzerIbfk2BackReferences")]
		public Benutzer BenutzerxbenutzerIbfk2 { get; set; }

		/// <summary>
		/// benutzerxbenutzer_ibfk_1
		/// </summary>
		[Association(ThisKey="Benutzereins", OtherKey="Nummer", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="benutzerxbenutzer_ibfk_1", BackReferenceName="Benutzerxbenutzeribfks")]
		public Benutzer Ibfk { get; set; }

		#endregion
	}

	[Table("bestellungen")]
	public partial class Bestellungen
	{
		[PrimaryKey, Identity   ] public uint      Nummer           { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public DateTime  Bestellzeitpunkt { get; set; } // datetime
		[Column,        Nullable] public DateTime? Abholzeitpunkt   { get; set; } // datetime
		[Column,        Nullable] public uint?     Benutzer         { get; set; } // int(10) unsigned
		[Column,        Nullable] public double?   Endpreis         { get; set; } // double(4,2)

		#region Associations

		/// <summary>
		/// bestellungen_ibfk_1
		/// </summary>
		[Association(ThisKey="Benutzer", OtherKey="Nummer", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="bestellungen_ibfk_1", BackReferenceName="Bestellungenibfks")]
		public Benutzer Ibfk { get; set; }

		/// <summary>
		/// mahlzeitenxbestellungen_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="Nummer", OtherKey="Bestellungen", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxbestellungen> Mahlzeitenxbestellungenibfks { get; set; }

		#endregion
	}

	[Table("bilder")]
	public partial class Bilder
	{
		[Column(),           PrimaryKey,  Identity] public uint   ID         { get; set; } // int(10) unsigned
		[Column("Alt-Text"), NotNull              ] public string AltText    { get; set; } // varchar(50)
		[Column(),              Nullable          ] public string Titel      { get; set; } // varchar(50)
		[Column(),           NotNull              ] public byte[] Bin�rdaten { get; set; } // mediumblob

		#region Associations

		/// <summary>
		/// BildDel_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Bild", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Kategorien> BildDels { get; set; }

		/// <summary>
		/// mahlzeitenxbilder_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Bilder", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxbilder> Mahlzeitenxbilderibfks { get; set; }

		#endregion
	}

	[Table("deklarationen")]
	public partial class Deklarationen
	{
		[PrimaryKey, NotNull] public string Zeichen      { get; set; } // varchar(2)
		[Column,     NotNull] public string Beschriftung { get; set; } // varchar(32)

		#region Associations

		/// <summary>
		/// mahlzeitenxdeklarationen_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="Zeichen", OtherKey="Deklarationen", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxdeklarationen> Mahlzeitenxdeklarationenibfks { get; set; }

		#endregion
	}

	[Table("fachbereiche")]
	public partial class Fachbereiche
	{
		[PrimaryKey, Identity   ] public uint   ID      { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public string Website { get; set; } // varchar(100)
		[Column,     NotNull    ] public string Name    { get; set; } // varchar(50)
		[Column,        Nullable] public string Adresse { get; set; } // varchar(100)

		#region Associations

		/// <summary>
		/// fhangeh�rigexfachbereiche_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Fachbereiche", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Fhangeh�rigexfachbereiche> Fhangeh�rigexfachbereicheibfk { get; set; }

		#endregion
	}

	[Table("fhangeh�rige")]
	public partial class Fhangeh�rige
	{
		[PrimaryKey, NotNull] public uint ID { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// BenutzerDelFHA
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Nummer", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="BenutzerDelFHA", BackReferenceName="BenutzerDelFHA")]
		public Benutzer Benutzer { get; set; }

		/// <summary>
		/// BenutzerDelMA_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Mitarbeiter BenutzerDelMA { get; set; }

		/// <summary>
		/// BenutzerDelStu_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Student BenutzerDelStu { get; set; }

		/// <summary>
		/// fhangeh�rigexfachbereiche_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="FHAngeh�rige", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Fhangeh�rigexfachbereiche> Fhangeh�rigexfachbereicheibfk { get; set; }

		#endregion
	}

	[Table("fhangeh�rigexfachbereiche")]
	public partial class Fhangeh�rigexfachbereiche
	{
		[Column, Nullable] public uint? Fachbereiche { get; set; } // int(10) unsigned
		[Column, Nullable] public uint? FHAngeh�rige { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// fhangeh�rigexfachbereiche_ibfk_2
		/// </summary>
		[Association(ThisKey="FHAngeh�rige", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="fhangeh�rigexfachbereiche_ibfk_2", BackReferenceName="Fhangeh�rigexfachbereicheibfk")]
		public Fhangeh�rige Fhangeh�rigexfachbereicheIbfk2 { get; set; }

		/// <summary>
		/// fhangeh�rigexfachbereiche_ibfk_1
		/// </summary>
		[Association(ThisKey="Fachbereiche", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="fhangeh�rigexfachbereiche_ibfk_1", BackReferenceName="Fhangeh�rigexfachbereicheibfk")]
		public Fachbereiche Ibfk { get; set; }

		#endregion
	}

	[Table("g�ste")]
	public partial class G�ste
	{
		[Column,        Nullable] public string    Grund       { get; set; } // varchar(255)
		[Column,        Nullable] public DateTime? Ablaufdatum { get; set; } // date
		[PrimaryKey, NotNull    ] public uint      ID          { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// BenutzerDelGast
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Nummer", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="BenutzerDelGast", BackReferenceName="BenutzerDelGast")]
		public Benutzer Benutzer { get; set; }

		#endregion
	}

	[Table("kategorien")]
	public partial class Kategorien
	{
		[PrimaryKey, Identity   ] public uint   ID          { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public string Bezeichnung { get; set; } // varchar(25)
		[Column,        Nullable] public uint?  Bild        { get; set; } // int(10) unsigned
		[Column,        Nullable] public uint?  Kategorie   { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// BildDel
		/// </summary>
		[Association(ThisKey="Bild", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="BildDel", BackReferenceName="BildDels")]
		public Bilder BildDel { get; set; }

		/// <summary>
		/// mahlzeiten_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Kategorie", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeiten> Mahlzeitenibfks { get; set; }

		/// <summary>
		/// ParentDel
		/// </summary>
		[Association(ThisKey="Kategorie", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="ParentDel", BackReferenceName="ParentDelBackReferences")]
		public Kategorien ParentDel { get; set; }

		/// <summary>
		/// ParentDel_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Kategorie", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Kategorien> ParentDelBackReferences { get; set; }

		#endregion
	}

	[Table("kommentare")]
	public partial class Kommentare
	{
		[PrimaryKey, Identity   ] public uint   ID         { get; set; } // int(10) unsigned
		[Column,        Nullable] public string Bemerkung  { get; set; } // varchar(255)
		[Column,     NotNull    ] public double Bewertung  { get; set; } // double(1,1)
		[Column,        Nullable] public uint?  Mahlzeiten { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public uint   Student    { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// kommentare_ibfk_1
		/// </summary>
		[Association(ThisKey="Student", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="kommentare_ibfk_1", BackReferenceName="Kommentareibfks")]
		public Student Ibfk { get; set; }

		/// <summary>
		/// MahlzeitGel�scht
		/// </summary>
		[Association(ThisKey="Mahlzeiten", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="MahlzeitGel�scht", BackReferenceName="MahlzeitGel�scht")]
		public Mahlzeiten MahlzeitGel�scht { get; set; }

		#endregion
	}

	[Table("mahlzeiten")]
	public partial class Mahlzeiten
	{
		[PrimaryKey, Identity   ] public uint   ID           { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public int    Vorrat       { get; set; } // int(11)
		[Column,     NotNull    ] public string Beschreibung { get; set; } // varchar(255)
		[Column,        Nullable] public uint?  Kategorie    { get; set; } // int(10) unsigned
		[Column,        Nullable] public bool?  Verf�gbar    { get; set; } // tinyint(1)
		[Column,        Nullable] public int?   PreisJahr    { get; set; } // year(4)
		[Column,     NotNull    ] public string Name         { get; set; } // varchar(20)

		#region Associations

		/// <summary>
		/// mahlzeiten_ibfk_1
		/// </summary>
		[Association(ThisKey="Kategorie", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="mahlzeiten_ibfk_1", BackReferenceName="Mahlzeitenibfks")]
		public Kategorien Ibfk { get; set; }

		/// <summary>
		/// mahlzeitenxbestellungen_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Mahlzeiten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxbestellungen> Mahlzeitenxbestellungenibfks { get; set; }

		/// <summary>
		/// mahlzeitenxbilder_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Mahlzeiten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxbilder> Mahlzeitenxbilderibfks { get; set; }

		/// <summary>
		/// mahlzeitenxdeklarationen_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Mahlzeiten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mahlzeitenxdeklarationen> Mahlzeitenxdeklarationenibfks { get; set; }

		/// <summary>
		/// MahlzeitGel�scht_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Mahlzeiten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Kommentare> MahlzeitGel�scht { get; set; }

		/// <summary>
		/// preise_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="MahlzeitenID", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Preise> Preiseibfks { get; set; }

		/// <summary>
		/// zutatenxmahlzeiten_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Mahlzeiten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Zutatenxmahlzeiten> Zutatenxmahlzeitenibfks { get; set; }

		#endregion
	}

	[Table("mahlzeitenxbestellungen")]
	public partial class Mahlzeitenxbestellungen
	{
		[Column,    Nullable] public uint? Mahlzeiten   { get; set; } // int(10) unsigned
		[Column,    Nullable] public uint? Bestellungen { get; set; } // int(10) unsigned
		[Column, NotNull    ] public uint  Anzahl       { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// mahlzeitenxbestellungen_ibfk_1
		/// </summary>
		[Association(ThisKey="Mahlzeiten", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxbestellungen_ibfk_1", BackReferenceName="Mahlzeitenxbestellungenibfks")]
		public Mahlzeiten Ibfk { get; set; }

		/// <summary>
		/// mahlzeitenxbestellungen_ibfk_2
		/// </summary>
		[Association(ThisKey="Bestellungen", OtherKey="Nummer", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxbestellungen_ibfk_2", BackReferenceName="Mahlzeitenxbestellungenibfks")]
		public Bestellungen MahlzeitenxbestellungenIbfk2 { get; set; }

		#endregion
	}

	[Table("mahlzeitenxbilder")]
	public partial class Mahlzeitenxbilder
	{
		[PrimaryKey, Identity] public uint ID         { get; set; } // int(10) unsigned
		[Column,     NotNull ] public uint Mahlzeiten { get; set; } // int(10) unsigned
		[Column,     NotNull ] public uint Bilder     { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// mahlzeitenxbilder_ibfk_1
		/// </summary>
		[Association(ThisKey="Mahlzeiten", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxbilder_ibfk_1", BackReferenceName="Mahlzeitenxbilderibfks")]
		public Mahlzeiten Ibfk { get; set; }

		/// <summary>
		/// mahlzeitenxbilder_ibfk_2
		/// </summary>
		[Association(ThisKey="Bilder", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxbilder_ibfk_2", BackReferenceName="Mahlzeitenxbilderibfks")]
		public Bilder MahlzeitenxbilderIbfk2 { get; set; }

		#endregion
	}

	[Table("mahlzeitenxdeklarationen")]
	public partial class Mahlzeitenxdeklarationen
	{
		[Column, Nullable] public uint?  Mahlzeiten    { get; set; } // int(10) unsigned
		[Column, Nullable] public string Deklarationen { get; set; } // varchar(2)

		#region Associations

		/// <summary>
		/// mahlzeitenxdeklarationen_ibfk_1
		/// </summary>
		[Association(ThisKey="Mahlzeiten", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxdeklarationen_ibfk_1", BackReferenceName="Mahlzeitenxdeklarationenibfks")]
		public Mahlzeiten Ibfk { get; set; }

		/// <summary>
		/// mahlzeitenxdeklarationen_ibfk_2
		/// </summary>
		[Association(ThisKey="Deklarationen", OtherKey="Zeichen", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="mahlzeitenxdeklarationen_ibfk_2", BackReferenceName="Mahlzeitenxdeklarationenibfks")]
		public Deklarationen MahlzeitenxdeklarationenIbfk2 { get; set; }

		#endregion
	}

	[Table("mitarbeiter")]
	public partial class Mitarbeiter
	{
		[PrimaryKey, NotNull    ] public uint   ID      { get; set; } // int(10) unsigned
		[Column,        Nullable] public string Telefon { get; set; } // varchar(20)
		[Column,        Nullable] public string B�ro    { get; set; } // varchar(20)

		#region Associations

		/// <summary>
		/// BenutzerDelMA
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="BenutzerDelMA", BackReferenceName="BenutzerDelMA")]
		public Fhangeh�rige Fhangeh�rige { get; set; }

		#endregion
	}

	[Table("preise")]
	public partial class Preise
	{
		[Column("MA-Preis"),    Nullable           ] public double? MaPreis      { get; set; } // double(4,2)
		[Column(),              Nullable           ] public double? Studentpreis { get; set; } // double(4,2)
		[Column(),                          NotNull] public double  Gastpreis    { get; set; } // double(4,2)
		[Column(),           PrimaryKey(2), NotNull] public int     Jahr         { get; set; } // year(4)
		[Column(),           PrimaryKey(1), NotNull] public uint    MahlzeitenID { get; set; } // int(5) unsigned

		#region Associations

		/// <summary>
		/// preise_ibfk_1
		/// </summary>
		[Association(ThisKey="MahlzeitenID", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="preise_ibfk_1", BackReferenceName="Preiseibfks")]
		public Mahlzeiten Mahlzeiten { get; set; }

		#endregion
	}

	[Table("student")]
	public partial class Student
	{
		[PrimaryKey, NotNull] public uint   ID             { get; set; } // int(10) unsigned
		[Column,     NotNull] public uint   Matrikelnummer { get; set; } // int(10) unsigned
		[Column,     NotNull] public string Studiengang    { get; set; } // enum('ET','INF','ISE','MCD','WI')

		#region Associations

		/// <summary>
		/// BenutzerDelStu
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="BenutzerDelStu", BackReferenceName="BenutzerDelStu")]
		public Fhangeh�rige Fhangeh�rige { get; set; }

		/// <summary>
		/// kommentare_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Student", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Kommentare> Kommentareibfks { get; set; }

		#endregion
	}

	[Table("zutaten")]
	public partial class Zutaten
	{
		[PrimaryKey, NotNull] public uint   ID          { get; set; } // int(10) unsigned
		[Column,     NotNull] public bool   Bio         { get; set; } // tinyint(1)
		[Column,     NotNull] public bool   Vegan       { get; set; } // tinyint(1)
		[Column,     NotNull] public bool   Vegetarisch { get; set; } // tinyint(1)
		[Column,     NotNull] public bool   Glutenfrei  { get; set; } // tinyint(1)
		[Column,     NotNull] public string Name        { get; set; } // varchar(30)

		#region Associations

		/// <summary>
		/// zutatenxmahlzeiten_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Zutaten", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Zutatenxmahlzeiten> Zutatenxmahlzeitenibfks { get; set; }

		#endregion
	}

	[Table("zutatenxmahlzeiten")]
	public partial class Zutatenxmahlzeiten
	{
		[PrimaryKey, Identity] public uint ID         { get; set; } // int(10) unsigned
		[Column,     NotNull ] public uint Zutaten    { get; set; } // int(10) unsigned
		[Column,     NotNull ] public uint Mahlzeiten { get; set; } // int(10) unsigned

		#region Associations

		/// <summary>
		/// zutatenxmahlzeiten_ibfk_1
		/// </summary>
		[Association(ThisKey="Zutaten", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="zutatenxmahlzeiten_ibfk_1", BackReferenceName="Zutatenxmahlzeitenibfks")]
		public Zutaten Ibfk { get; set; }

		/// <summary>
		/// zutatenxmahlzeiten_ibfk_2
		/// </summary>
		[Association(ThisKey="Mahlzeiten", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="zutatenxmahlzeiten_ibfk_2", BackReferenceName="Zutatenxmahlzeitenibfks")]
		public Mahlzeiten ZutatenxmahlzeitenIbfk2 { get; set; }

		#endregion
	}

	[Table("zutatjoin", IsView=true)]
	public partial class Zutatjoin
	{
		[Column("vegetarisch"),    Nullable] public bool? Vegetarisch { get; set; } // tinyint(1)
		[Column("vegan"),          Nullable] public bool? Vegan       { get; set; } // tinyint(1)
		[Column(),              NotNull    ] public uint  Mahlzeiten  { get; set; } // int(10) unsigned
	}

	public static partial class MensaStoredProcedures
	{
		#region Nutzerrolle

		public static IEnumerable<NutzerrolleResult> Nutzerrolle(this DataConnection dataConnection, int? parameter_nummer)
		{
			return dataConnection.QueryProc<NutzerrolleResult>("`Nutzerrolle`",
				new DataParameter("parameter_nummer", parameter_nummer, DataType.Int32));
		}

		public partial class NutzerrolleResult
		{
			public string Rolle { get; set; }
		}

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Benutzer Find(this ITable<Benutzer> table, uint Nummer)
		{
			return table.FirstOrDefault(t =>
				t.Nummer == Nummer);
		}

		public static Bestellungen Find(this ITable<Bestellungen> table, uint Nummer)
		{
			return table.FirstOrDefault(t =>
				t.Nummer == Nummer);
		}

		public static Bilder Find(this ITable<Bilder> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Deklarationen Find(this ITable<Deklarationen> table, string Zeichen)
		{
			return table.FirstOrDefault(t =>
				t.Zeichen == Zeichen);
		}

		public static Fachbereiche Find(this ITable<Fachbereiche> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Fhangeh�rige Find(this ITable<Fhangeh�rige> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static G�ste Find(this ITable<G�ste> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Kategorien Find(this ITable<Kategorien> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Kommentare Find(this ITable<Kommentare> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Mahlzeiten Find(this ITable<Mahlzeiten> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Mahlzeitenxbilder Find(this ITable<Mahlzeitenxbilder> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Mitarbeiter Find(this ITable<Mitarbeiter> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Preise Find(this ITable<Preise> table, int Jahr, uint MahlzeitenID)
		{
			return table.FirstOrDefault(t =>
				t.Jahr         == Jahr &&
				t.MahlzeitenID == MahlzeitenID);
		}

		public static Student Find(this ITable<Student> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Zutaten Find(this ITable<Zutaten> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Zutatenxmahlzeiten Find(this ITable<Zutatenxmahlzeiten> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}
	}
}
