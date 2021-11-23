﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PERSONAL_TRAKING")]
	public partial class EmployeeDataClassDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertDEPARTMENT(DEPARTMENT instance);
    partial void UpdateDEPARTMENT(DEPARTMENT instance);
    partial void DeleteDEPARTMENT(DEPARTMENT instance);
    partial void InsertPOSITION(POSITION instance);
    partial void UpdatePOSITION(POSITION instance);
    partial void DeletePOSITION(POSITION instance);
    #endregion
		
		public EmployeeDataClassDataContext() : 
				base(global::DAL.Properties.Settings.Default.PERSONAL_TRAKINGConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDataClassDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDataClassDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDataClassDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDataClassDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<EMPLOYEE> EMPLOYEE
		{
			get
			{
				return this.GetTable<EMPLOYEE>();
			}
		}
		
		public System.Data.Linq.Table<MONTH> MONTH
		{
			get
			{
				return this.GetTable<MONTH>();
			}
		}
		
		public System.Data.Linq.Table<PERMISSION> PERMISSION
		{
			get
			{
				return this.GetTable<PERMISSION>();
			}
		}
		
		public System.Data.Linq.Table<PERMISSIONSTATE> PERMISSIONSTATE
		{
			get
			{
				return this.GetTable<PERMISSIONSTATE>();
			}
		}
		
		public System.Data.Linq.Table<TASKSTATE> TASKSTATE
		{
			get
			{
				return this.GetTable<TASKSTATE>();
			}
		}
		
		public System.Data.Linq.Table<SALARY> SALARY
		{
			get
			{
				return this.GetTable<SALARY>();
			}
		}
		
		public System.Data.Linq.Table<TASK> TASK
		{
			get
			{
				return this.GetTable<TASK>();
			}
		}
		
		public System.Data.Linq.Table<DEPARTMENT> DEPARTMENT
		{
			get
			{
				return this.GetTable<DEPARTMENT>();
			}
		}
		
		public System.Data.Linq.Table<POSITION> POSITION
		{
			get
			{
				return this.GetTable<POSITION>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EMPLOYEE")]
	public partial class EMPLOYEE
	{
		
		private int _ID_Employee;
		
		private int _UserNo;
		
		private string _Name;
		
		private string _SurName;
		
		private string _ImagePath;
		
		private int _ID_Department;
		
		private int _ID_Position;
		
		private int _Salary;
		
		private System.Nullable<System.DateTime> _BirthDay;
		
		private string _Adress;
		
		private string _Password;
		
		private System.Nullable<bool> _isAdmin;
		
		public EMPLOYEE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Employee", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_Employee
		{
			get
			{
				return this._ID_Employee;
			}
			set
			{
				if ((this._ID_Employee != value))
				{
					this._ID_Employee = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserNo", DbType="Int NOT NULL")]
		public int UserNo
		{
			get
			{
				return this._UserNo;
			}
			set
			{
				if ((this._UserNo != value))
				{
					this._UserNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SurName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SurName
		{
			get
			{
				return this._SurName;
			}
			set
			{
				if ((this._SurName != value))
				{
					this._SurName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImagePath", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				if ((this._ImagePath != value))
				{
					this._ImagePath = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Department", DbType="Int NOT NULL")]
		public int ID_Department
		{
			get
			{
				return this._ID_Department;
			}
			set
			{
				if ((this._ID_Department != value))
				{
					this._ID_Department = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Position", DbType="Int NOT NULL")]
		public int ID_Position
		{
			get
			{
				return this._ID_Position;
			}
			set
			{
				if ((this._ID_Position != value))
				{
					this._ID_Position = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Salary", DbType="Int NOT NULL")]
		public int Salary
		{
			get
			{
				return this._Salary;
			}
			set
			{
				if ((this._Salary != value))
				{
					this._Salary = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDay", DbType="Date")]
		public System.Nullable<System.DateTime> BirthDay
		{
			get
			{
				return this._BirthDay;
			}
			set
			{
				if ((this._BirthDay != value))
				{
					this._BirthDay = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Adress", DbType="VarChar(MAX)")]
		public string Adress
		{
			get
			{
				return this._Adress;
			}
			set
			{
				if ((this._Adress != value))
				{
					this._Adress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(20)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isAdmin", DbType="Bit")]
		public System.Nullable<bool> isAdmin
		{
			get
			{
				return this._isAdmin;
			}
			set
			{
				if ((this._isAdmin != value))
				{
					this._isAdmin = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MONTH")]
	public partial class MONTH
	{
		
		private int _Id_Month;
		
		private string _MonthName;
		
		public MONTH()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id_Month", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int Id_Month
		{
			get
			{
				return this._Id_Month;
			}
			set
			{
				if ((this._Id_Month != value))
				{
					this._Id_Month = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MonthName", DbType="VarChar(20)")]
		public string MonthName
		{
			get
			{
				return this._MonthName;
			}
			set
			{
				if ((this._MonthName != value))
				{
					this._MonthName = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PERMISSION")]
	public partial class PERMISSION
	{
		
		private int _ID_Permission;
		
		private int _ID_Employee;
		
		private System.DateTime _PermissionStartDate;
		
		private System.DateTime _PermissionEndDate;
		
		private int _PermissionState;
		
		private string _PermissionExplanation;
		
		private int _PermissionDay;
		
		public PERMISSION()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Permission", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_Permission
		{
			get
			{
				return this._ID_Permission;
			}
			set
			{
				if ((this._ID_Permission != value))
				{
					this._ID_Permission = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Employee", DbType="Int NOT NULL")]
		public int ID_Employee
		{
			get
			{
				return this._ID_Employee;
			}
			set
			{
				if ((this._ID_Employee != value))
				{
					this._ID_Employee = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionStartDate", DbType="Date NOT NULL")]
		public System.DateTime PermissionStartDate
		{
			get
			{
				return this._PermissionStartDate;
			}
			set
			{
				if ((this._PermissionStartDate != value))
				{
					this._PermissionStartDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionEndDate", DbType="Date NOT NULL")]
		public System.DateTime PermissionEndDate
		{
			get
			{
				return this._PermissionEndDate;
			}
			set
			{
				if ((this._PermissionEndDate != value))
				{
					this._PermissionEndDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionState", DbType="Int NOT NULL")]
		public int PermissionState
		{
			get
			{
				return this._PermissionState;
			}
			set
			{
				if ((this._PermissionState != value))
				{
					this._PermissionState = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionExplanation", DbType="NVarChar(MAX)")]
		public string PermissionExplanation
		{
			get
			{
				return this._PermissionExplanation;
			}
			set
			{
				if ((this._PermissionExplanation != value))
				{
					this._PermissionExplanation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionDay", DbType="Int NOT NULL")]
		public int PermissionDay
		{
			get
			{
				return this._PermissionDay;
			}
			set
			{
				if ((this._PermissionDay != value))
				{
					this._PermissionDay = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PERMISSIONSTATE")]
	public partial class PERMISSIONSTATE
	{
		
		private int _ID_PermissionState;
		
		private string _StateName;
		
		public PERMISSIONSTATE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_PermissionState", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_PermissionState
		{
			get
			{
				return this._ID_PermissionState;
			}
			set
			{
				if ((this._ID_PermissionState != value))
				{
					this._ID_PermissionState = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StateName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string StateName
		{
			get
			{
				return this._StateName;
			}
			set
			{
				if ((this._StateName != value))
				{
					this._StateName = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TASKSTATE")]
	public partial class TASKSTATE
	{
		
		private int _Id_TaskEstate;
		
		private string _StateName;
		
		public TASKSTATE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id_TaskEstate", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int Id_TaskEstate
		{
			get
			{
				return this._Id_TaskEstate;
			}
			set
			{
				if ((this._Id_TaskEstate != value))
				{
					this._Id_TaskEstate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StateName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string StateName
		{
			get
			{
				return this._StateName;
			}
			set
			{
				if ((this._StateName != value))
				{
					this._StateName = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SALARY")]
	public partial class SALARY
	{
		
		private int _ID_Salary;
		
		private int _ID_Employee;
		
		private int _Amount;
		
		private int _Year;
		
		private System.Nullable<int> _Id_Month;
		
		public SALARY()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Salary", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_Salary
		{
			get
			{
				return this._ID_Salary;
			}
			set
			{
				if ((this._ID_Salary != value))
				{
					this._ID_Salary = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Employee", DbType="Int NOT NULL")]
		public int ID_Employee
		{
			get
			{
				return this._ID_Employee;
			}
			set
			{
				if ((this._ID_Employee != value))
				{
					this._ID_Employee = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this._Amount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int NOT NULL")]
		public int Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this._Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id_Month", DbType="Int")]
		public System.Nullable<int> Id_Month
		{
			get
			{
				return this._Id_Month;
			}
			set
			{
				if ((this._Id_Month != value))
				{
					this._Id_Month = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TASK")]
	public partial class TASK
	{
		
		private int _ID_Task;
		
		private int _ID_Employee;
		
		private string _TaskTitle;
		
		private string _TaskContent;
		
		private System.Nullable<System.DateTime> _TaskStartDate;
		
		private System.Nullable<System.DateTime> _TaskDeliveryDate;
		
		private System.Nullable<int> _TaskState;
		
		public TASK()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Task", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_Task
		{
			get
			{
				return this._ID_Task;
			}
			set
			{
				if ((this._ID_Task != value))
				{
					this._ID_Task = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_Employee", DbType="Int NOT NULL")]
		public int ID_Employee
		{
			get
			{
				return this._ID_Employee;
			}
			set
			{
				if ((this._ID_Employee != value))
				{
					this._ID_Employee = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskTitle", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string TaskTitle
		{
			get
			{
				return this._TaskTitle;
			}
			set
			{
				if ((this._TaskTitle != value))
				{
					this._TaskTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskContent", DbType="VarChar(MAX)")]
		public string TaskContent
		{
			get
			{
				return this._TaskContent;
			}
			set
			{
				if ((this._TaskContent != value))
				{
					this._TaskContent = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskStartDate", DbType="Date")]
		public System.Nullable<System.DateTime> TaskStartDate
		{
			get
			{
				return this._TaskStartDate;
			}
			set
			{
				if ((this._TaskStartDate != value))
				{
					this._TaskStartDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskDeliveryDate", DbType="Date")]
		public System.Nullable<System.DateTime> TaskDeliveryDate
		{
			get
			{
				return this._TaskDeliveryDate;
			}
			set
			{
				if ((this._TaskDeliveryDate != value))
				{
					this._TaskDeliveryDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskState", DbType="Int")]
		public System.Nullable<int> TaskState
		{
			get
			{
				return this._TaskState;
			}
			set
			{
				if ((this._TaskState != value))
				{
					this._TaskState = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DEPARTMENT")]
	public partial class DEPARTMENT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _DepartmentName;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDepartmentNameChanging(string value);
    partial void OnDepartmentNameChanged();
    #endregion
		
		public DEPARTMENT()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this.OnDepartmentNameChanging(value);
					this.SendPropertyChanging();
					this._DepartmentName = value;
					this.SendPropertyChanged("DepartmentName");
					this.OnDepartmentNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.POSITION")]
	public partial class POSITION : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _PositionName;
		
		private System.Nullable<int> _Deparment_ID;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnPositionNameChanging(string value);
    partial void OnPositionNameChanged();
    partial void OnDeparment_IDChanging(System.Nullable<int> value);
    partial void OnDeparment_IDChanged();
    #endregion
		
		public POSITION()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionName", DbType="VarChar(50)")]
		public string PositionName
		{
			get
			{
				return this._PositionName;
			}
			set
			{
				if ((this._PositionName != value))
				{
					this.OnPositionNameChanging(value);
					this.SendPropertyChanging();
					this._PositionName = value;
					this.SendPropertyChanged("PositionName");
					this.OnPositionNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deparment_ID", DbType="Int")]
		public System.Nullable<int> Deparment_ID
		{
			get
			{
				return this._Deparment_ID;
			}
			set
			{
				if ((this._Deparment_ID != value))
				{
					this.OnDeparment_IDChanging(value);
					this.SendPropertyChanging();
					this._Deparment_ID = value;
					this.SendPropertyChanged("Deparment_ID");
					this.OnDeparment_IDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591