namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentWorkspace : SObject
	{
		public string Name {set;get;}

		public string Description {set;get;}

		public string TagModel {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string DefaultRecordTypeId {set;get;}

		public bool IsRestrictContentTypes {set;get;}

		public bool IsRestrictLinkedContentTypes {set;get;}

		public string WorkspaceType {set;get;}

		public bool ShouldAddCreatorMembership {set;get;}

		public DateTime LastWorkspaceActivityDate {set;get;}

		public string RootContentFolderId {set;get;}

		public string NamespacePrefix {set;get;}

		public string DeveloperName {set;get;}
	}
}