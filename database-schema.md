# Database Schema (SQL Server)

## Students
| Column     | Type       | Notes                 |
|------------|------------|-----------------------|
| Id         | int (PK)   |                       |
| Name       | nvarchar   |                       |
| Email      | nvarchar   |                       |
| CreatedAt  | datetime2  |                       |

## Subjects
| Id         | int (PK)   |
| Name       | nvarchar   |

## SubjectSections (Notes, Questions, CATs, Exams)
| Id, SubjectId(FK), SectionType, DisplayOrder, Name

... (add others as we build)