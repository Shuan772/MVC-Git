//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/t4models).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : praktikum
	/// Data Source    : localhost
	/// Server Version : 5.5.5-10.2.9-MariaDB
	/// </summary>
	public partial class PraktikumDB : LinqToDB.Data.DataConnection
	{
		public ITable<Bestellung>         Bestellungs        { get { return this.GetTable<Bestellung>(); } }
		public ITable<BestellungProduktX> BestellungProduktX { get { return this.GetTable<BestellungProduktX>(); } }
		public ITable<Bild>               Bilds              { get { return this.GetTable<Bild>(); } }
		public ITable<FeNutzer>           FeNutzer           { get { return this.GetTable<FeNutzer>(); } }
		public ITable<FhAngeh�rige>       FhAngeh�rige       { get { return this.GetTable<FhAngeh�rige>(); } }
		public ITable<Gast>               Gasts              { get { return this.GetTable<Gast>(); } }
		public ITable<Kategorie>          Kategories         { get { return this.GetTable<Kategorie>(); } }
		public ITable<Mitarbeiter>        Mitarbeiters       { get { return this.GetTable<Mitarbeiter>(); } }
		public ITable<Preis>              Preis              { get { return this.GetTable<Preis>(); } }
		public ITable<Produkt>            Produkts           { get { return this.GetTable<Produkt>(); } }
		public ITable<ProduktZutatX>      ProduktZutatX      { get { return this.GetTable<ProduktZutatX>(); } }
		public ITable<Student>            Students           { get { return this.GetTable<Student>(); } }
		public ITable<Zutat>              Zutats             { get { return this.GetTable<Zutat>(); } }

		public PraktikumDB()
		{
			InitDataContext();
		}

		public PraktikumDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		partial void InitDataContext();
	}

	[Table("bestellung")]
	public partial class Bestellung
	{
		[PrimaryKey, NotNull] public uint     ID        { get; set; } // int(10) unsigned
		[Column,     NotNull] public uint     FKID      { get; set; } // mediumint(8) unsigned
		[Column,     NotNull] public DateTime Zeitpunkt { get; set; } // timestamp

		#region Associations

		/// <summary>
		/// bestellung_produkt_x_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Bestellungid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<BestellungProduktX> BestellungProduktX { get; set; }

		/// <summary>
		/// bestellung_ibfk_1
		/// </summary>
		[Association(ThisKey="FKID", OtherKey="NR", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="bestellung_ibfk_1", BackReferenceName="Bestellungs")]
		public FeNutzer FeNutzer { get; set; }

		#endregion
	}

	[Table("bestellung_produkt_x")]
	public partial class BestellungProduktX
	{
		[Column("bestellungid"), PrimaryKey(1), NotNull] public uint  Bestellungid { get; set; } // int(10) unsigned
		[Column("produktid"),    PrimaryKey(2), NotNull] public uint  Produktid    { get; set; } // mediumint(8) unsigned
		[Column("menge"),                       NotNull] public sbyte Menge        { get; set; } // tinyint(4)

		#region Associations

		/// <summary>
		/// bestellung_produkt_x_ibfk_1
		/// </summary>
		[Association(ThisKey="Bestellungid", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="bestellung_produkt_x_ibfk_1", BackReferenceName="BestellungProduktX")]
		public Bestellung Bestellung { get; set; }

		/// <summary>
		/// bestellung_produkt_x_ibfk_2
		/// </summary>
		[Association(ThisKey="Produktid", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="bestellung_produkt_x_ibfk_2", BackReferenceName="BestellungProduktX")]
		public Produkt Produkt { get; set; }

		#endregion
	}

	[Table("bild")]
	public partial class Bild
	{
		[Column("id"),          PrimaryKey,  Identity] public uint   Id               { get; set; } // mediumint(8) unsigned
		[Column("fkprodukt"),      Nullable          ] public uint?  Fkprodukt        { get; set; } // mediumint(8) unsigned
		[Column("fkkategorie"),    Nullable          ] public uint?  Fkkategorie      { get; set; } // mediumint(8) unsigned
		[Column(),              NotNull              ] public byte[] Binaerdaten      { get; set; } // blob
		[Column(),              NotNull              ] public string Alttext          { get; set; } // varchar(60)
		[Column(),              NotNull              ] public string Titel            { get; set; } // varchar(60)
		[Column(),                 Nullable          ] public string Bildunterschrift { get; set; } // varchar(80)

		#region Associations

		/// <summary>
		/// produkt_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="Bildid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Produkt> Produkts { get; set; }

		#endregion
	}

	[Table("fe-nutzer")]
	public partial class FeNutzer
	{
		[PrimaryKey, Identity   ] public uint      NR           { get; set; } // mediumint(8) unsigned
		[Column,     NotNull    ] public string    Email        { get; set; } // varchar(100)
		[Column,     NotNull    ] public string    Loginname    { get; set; } // varchar(60)
		[Column,     NotNull    ] public string    Vorname      { get; set; } // varchar(60)
		[Column,     NotNull    ] public string    Nachname     { get; set; } // varchar(60)
		[Column,     NotNull    ] public bool      Aktiv        { get; set; } // tinyint(1)
		[Column,     NotNull    ] public DateTime  Anlegedatum  { get; set; } // datetime
		[Column,        Nullable] public DateTime? LetzterLogin { get; set; } // datetime
		[Column,     NotNull    ] public string    Hash         { get; set; } // char(50)
		[Column,     NotNull    ] public string    Salt         { get; set; } // char(32)
		[Column,     NotNull    ] public uint      Stretch      { get; set; } // int(10) unsigned
		[Column,     NotNull    ] public string    Algorithmus  { get; set; } // varchar(6)

		#region Associations

		/// <summary>
		/// bestellung_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="NR", OtherKey="FKID", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Bestellung> Bestellungs { get; set; }

		/// <summary>
		/// fh-angeh�rige_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="NR", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public FhAngeh�rige FhAngeh�rige { get; set; }

		/// <summary>
		/// gast_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="NR", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Gast Gast { get; set; }

		#endregion
	}

	[Table("fh-angeh�rige")]
	public partial class FhAngeh�rige
	{
		[PrimaryKey, NotNull] public uint ID { get; set; } // mediumint(8) unsigned

		#region Associations

		/// <summary>
		/// fh-angeh�rige_ibfk_1
		/// </summary>
		[Association(ThisKey="ID", OtherKey="NR", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="fh-angeh�rige_ibfk_1", BackReferenceName="FhAngeh�rige")]
		public FeNutzer FeNutzer { get; set; }

		/// <summary>
		/// mitarbeiter_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Fkfh", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Mitarbeiter> Mitarbeiters { get; set; }

		/// <summary>
		/// student_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Student Student { get; set; }

		#endregion
	}

	[Table("gast")]
	public partial class Gast
	{
		[PrimaryKey, NotNull] public uint     ID     { get; set; } // mediumint(8) unsigned
		[Column,     NotNull] public string   Grund  { get; set; } // varchar(60)
		[Column,     NotNull] public DateTime Ablauf { get; set; } // datetime

		#region Associations

		/// <summary>
		/// gast_ibfk_1
		/// </summary>
		[Association(ThisKey="ID", OtherKey="NR", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="gast_ibfk_1", BackReferenceName="Gast")]
		public FeNutzer FeNutzer { get; set; }

		#endregion
	}

	[Table("kategorie")]
	public partial class Kategorie
	{
		[PrimaryKey, Identity   ] public uint   ID            { get; set; } // mediumint(8) unsigned
		[Column,     NotNull    ] public string Bezeichnung   { get; set; } // varchar(100)
		[Column,        Nullable] public uint?  Oberkategorie { get; set; } // mediumint(8) unsigned

		#region Associations

		/// <summary>
		/// OberKat
		/// </summary>
		[Association(ThisKey="Oberkategorie", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="OberKat", BackReferenceName="Kategories")]
		public Kategorie FKKategorie { get; set; }

		/// <summary>
		/// OberKat_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Oberkategorie", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Kategorie> Kategories { get; set; }

		/// <summary>
		/// produkt_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Katid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Produkt> Produkts { get; set; }

		#endregion
	}

	[Table("mitarbeiter")]
	public partial class Mitarbeiter
	{
		[Column("fkfh"),              NotNull    ] public uint   Fkfh              { get; set; } // mediumint(8) unsigned
		[Column("mitarbeiternummer"),    Nullable] public uint?  Mitarbeiternummer { get; set; } // mediumint(8) unsigned
		[Column("telefonnummer"),        Nullable] public string Telefonnummer     { get; set; } // varchar(25)
		[Column(),                       Nullable] public string B�ro              { get; set; } // varchar(25)

		#region Associations

		/// <summary>
		/// mitarbeiter_ibfk_1
		/// </summary>
		[Association(ThisKey="Fkfh", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="mitarbeiter_ibfk_1", BackReferenceName="Mitarbeiters")]
		public FhAngeh�rige FhAngeh�rige { get; set; }

		#endregion
	}

	[Table("preis")]
	public partial class Preis
	{
		[PrimaryKey, Identity] public uint    ID                { get; set; } // mediumint(8) unsigned
		[Column,     NotNull ] public decimal Mitarbeiterbetrag { get; set; } // decimal(4,2)
		[Column,     NotNull ] public decimal Studentenbetrag   { get; set; } // decimal(4,2)
		[Column,     NotNull ] public decimal Gastbetrag        { get; set; } // decimal(4,2)

		#region Associations

		/// <summary>
		/// preis_ibfk_1
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="preis_ibfk_1", BackReferenceName="Prei")]
		public Produkt Produkt { get; set; }

		#endregion
	}

	[Table("produkt")]
	public partial class Produkt
	{
		[Column(),         PrimaryKey,  Identity] public uint   ID           { get; set; } // mediumint(8) unsigned
		[Column(),         NotNull              ] public string Beschreibung { get; set; } // varchar(80)
		[Column("katid"),     Nullable          ] public uint?  Katid        { get; set; } // mediumint(8) unsigned
		[Column("bildid"),    Nullable          ] public uint?  Bildid       { get; set; } // mediumint(8) unsigned

		#region Associations

		/// <summary>
		/// bestellung_produkt_x_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Produktid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<BestellungProduktX> BestellungProduktX { get; set; }

		/// <summary>
		/// produkt_ibfk_2
		/// </summary>
		[Association(ThisKey="Bildid", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="produkt_ibfk_2", BackReferenceName="Produkts")]
		public Bild Bild { get; set; }

		/// <summary>
		/// produkt_ibfk_1
		/// </summary>
		[Association(ThisKey="Katid", OtherKey="ID", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="produkt_ibfk_1", BackReferenceName="Produkts")]
		public Kategorie Kategorie { get; set; }

		/// <summary>
		/// preis_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Preis Prei { get; set; }

		/// <summary>
		/// produkt_zutat_x_ibfk_1_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Produktid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<ProduktZutatX> ProduktZutatX { get; set; }

		#endregion
	}

	[Table("produkt_zutat_x")]
	public partial class ProduktZutatX
	{
		[Column("produktid"), PrimaryKey(1), NotNull] public uint   Produktid { get; set; } // mediumint(8) unsigned
		[Column("zutatid"),   PrimaryKey(2), NotNull] public ushort Zutatid   { get; set; } // smallint(5) unsigned

		#region Associations

		/// <summary>
		/// produkt_zutat_x_ibfk_1
		/// </summary>
		[Association(ThisKey="Produktid", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="produkt_zutat_x_ibfk_1", BackReferenceName="ProduktZutatX")]
		public Produkt Produkt { get; set; }

		/// <summary>
		/// produkt_zutat_x_ibfk_2
		/// </summary>
		[Association(ThisKey="Zutatid", OtherKey="ID", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="produkt_zutat_x_ibfk_2", BackReferenceName="ProduktZutatX")]
		public Zutat Zutat { get; set; }

		#endregion
	}

	[Table("student")]
	public partial class Student
	{
		[PrimaryKey, NotNull] public uint   ID             { get; set; } // mediumint(8) unsigned
		[Column,     NotNull] public uint   Matrikelnummer { get; set; } // mediumint(8) unsigned
		[Column,     NotNull] public string Studiengang    { get; set; } // varchar(30)

		#region Associations

		/// <summary>
		/// student_ibfk_1
		/// </summary>
		[Association(ThisKey="ID", OtherKey="ID", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="student_ibfk_1", BackReferenceName="Student")]
		public FhAngeh�rige FhAngeh�rige { get; set; }

		#endregion
	}

	[Table("zutat")]
	public partial class Zutat
	{
		[PrimaryKey, Identity   ] public ushort ID           { get; set; } // smallint(5) unsigned
		[Column,     NotNull    ] public string Name         { get; set; } // varchar(30)
		[Column,        Nullable] public string Beschreibung { get; set; } // varchar(80)
		[Column,        Nullable] public sbyte? Glutenfrei   { get; set; } // tinyint(4)
		[Column,        Nullable] public sbyte? Bio          { get; set; } // tinyint(4)
		[Column,        Nullable] public sbyte? Vegetarisch  { get; set; } // tinyint(4)
		[Column,        Nullable] public sbyte? Vegan        { get; set; } // tinyint(4)

		#region Associations

		/// <summary>
		/// produkt_zutat_x_ibfk_2_BackReference
		/// </summary>
		[Association(ThisKey="ID", OtherKey="Zutatid", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<ProduktZutatX> ProduktZutatX { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Bestellung Find(this ITable<Bestellung> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static BestellungProduktX Find(this ITable<BestellungProduktX> table, uint Bestellungid, uint Produktid)
		{
			return table.FirstOrDefault(t =>
				t.Bestellungid == Bestellungid &&
				t.Produktid    == Produktid);
		}

		public static Bild Find(this ITable<Bild> table, uint Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FeNutzer Find(this ITable<FeNutzer> table, uint NR)
		{
			return table.FirstOrDefault(t =>
				t.NR == NR);
		}

		public static FhAngeh�rige Find(this ITable<FhAngeh�rige> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Gast Find(this ITable<Gast> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Kategorie Find(this ITable<Kategorie> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Preis Find(this ITable<Preis> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Produkt Find(this ITable<Produkt> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static ProduktZutatX Find(this ITable<ProduktZutatX> table, uint Produktid, ushort Zutatid)
		{
			return table.FirstOrDefault(t =>
				t.Produktid == Produktid &&
				t.Zutatid   == Zutatid);
		}

		public static Student Find(this ITable<Student> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Zutat Find(this ITable<Zutat> table, ushort ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}
	}
}
