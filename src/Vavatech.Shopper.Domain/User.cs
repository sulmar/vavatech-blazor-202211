﻿namespace Vavatech.Shopper.Domain;

public class User : BaseEntity
{    
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }

}
