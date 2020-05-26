import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Course, NewCourse } from './interfaces/course';


@Injectable()
export class CourseDataService {
    constructor(private http: HttpClient) { }

  getAllCourses() {
    return this.http.get<Course[]>('/api/course');

  }

  addCourse(newCourse: NewCourse) {
    return this.http.post<NewCourse>('/api/course/new',newCourse);
  }
  //getCoursesById(id: number): {
  //  return this.http.get<Course[]>(`/api/course/${id}`);
  //}
}