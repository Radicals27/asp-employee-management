using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Classes
{
	public class Employee
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Position is required")]
		public string Position { get; set; }
	}
}