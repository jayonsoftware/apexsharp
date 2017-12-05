namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Vote : SObject
	{
		public bool IsDeleted {set;get;}

		public string ParentId {set;get;}

		public Idea Parent {set;get;}

		public string Type {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}