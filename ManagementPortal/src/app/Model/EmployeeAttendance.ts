export class EmployeeAttendance {
    Id: number;
    EmployeeId: number;
    CheckIn: string;
    CheckOut: string;
    IsPresent: boolean;
    Duration: number;
}

export class EmployeeWorkingResult {
    EmployeeId: number;
    TotalWorkingDays: number;
    QueryStartTime: Date;
    QueryEndTime: Date;
}