using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Responses;

public class ResponseApi<T>
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }
}
