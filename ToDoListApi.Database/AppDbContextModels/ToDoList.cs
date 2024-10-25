﻿using System;
using System.Collections.Generic;

namespace ToDoListApi.Database.AppDbContextModels;

public partial class ToDoList
{
    public int TaskId { get; set; }

    public string TaskTitle { get; set; } = null!;

    public string? TaskDescription { get; set; }

    public int? CategoryId { get; set; }

    public byte? PriorityLevel { get; set; }

    public string? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? CompletedDate { get; set; }
}