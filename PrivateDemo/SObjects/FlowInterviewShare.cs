namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FlowInterviewShare : SObject
	{
		public string ParentId {set;get;}

		public FlowInterview Parent {set;get;}

		public string UserOrGroupId {set;get;}

		public Group UserOrGroup {set;get;}

		public string AccessLevel {set;get;}

		public string RowCause {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public bool IsDeleted {set;get;}
	}
}