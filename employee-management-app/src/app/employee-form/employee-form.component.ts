import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../employee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})

export class EmployeeFormComponent implements OnInit {
  employee: Employee = {
    id: 0,
    firstName: '',
    lastName: '',
    phone: '',
    email: '',
    position: ''
  }

  isEditing: boolean = false;

  errorMessage: string = "";

  constructor(private employeeService: EmployeeService, 
    private router: Router,
    private route: ActivatedRoute,
    ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((result) => {

      const id = result.get('id');

      if(id != null) {
        this.isEditing = true;   // If there is a employee ID param, we are editing one.
        this.employeeService.getEmployeeById(Number(id)).subscribe({
          next: (result) => this.employee = result,
          error: (err) => this.errorMessage = `Error occured: ${err.status}`
        });
      }
    });
  }

  onSubmit() : void {
    
    if(this.isEditing) {  // Edit an employee
      this.employeeService.editEmployee(this.employee)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/']);  // Navigate to home page on successful creation
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = `Error occured during edit: ${err.status}`;
        }
      });
    }
    else {  // Create a new employee
      this.employeeService.createEmployee(this.employee)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/']);  // Navigate to home page on successful creation
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = `Error occured during create: ${err.status}`;
        }
      });
    }
  }
}