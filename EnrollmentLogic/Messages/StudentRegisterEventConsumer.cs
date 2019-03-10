using EnrollmentApi.Logic.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnrollmentApi.Logic.Messages
{
    public class StudentRegisterEventConsumer : IConsumer<StudentRegisterEvent>
    {
        public async Task Consume(ConsumeContext<StudentRegisterEvent> context)
        {
            Console.WriteLine($"StudentRegisterEvent:{context.Message.Name}");            
        }
    }
}
