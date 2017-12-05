namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SecondChild__c : SObject
	{
		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string FirstChild__c {set;get;}

		public FirstChild__c FirstChild__r {set;get;}
	}
}