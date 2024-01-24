using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketsMABB3.DAL;
using TicketsMABB3.Models;

namespace TicketsMABB3.BLLoServices
{
    public class ServicesTickets
    {
        public class TicketsBLL
        {
            public readonly Contexto Contexto;

            public TicketsBLL(Contexto contexto)
            {
                Contexto = contexto;
            }

            public bool Existe(int id)
            {
                return Contexto.Tickets.Any(o => o.TicketId == id);
            }

            private bool Insertar(Tickets Tickets)
            {
                Contexto.Tickets.Add(Tickets);
                return Contexto.SaveChanges() > 0;
            }

            public bool Modificar(Tickets tickets)
            {
                var PrioridadADesechar = Contexto.Tickets.Find(tickets.TicketId);
                if (tickets != null)
                {
                    Contexto.Entry(PrioridadADesechar).State = EntityState.Detached;
                    Contexto.Entry(tickets).State = EntityState.Modified;
                    return Contexto.SaveChanges() > 0;
                }
                return false;

            }

            public bool Guardar(Tickets tickets)
            {
                if (Existe(tickets.TicketId))
                {
                    // El ticket ya existe, intenta modificarlo
                    return Modificar(tickets);
                }
                else
                {
                    // El ticket no existe, intenta insertarlo
                    return Insertar(tickets);
                }
            }

            public bool Eliminar(Tickets tickets)
            {


                if (tickets != null)
                {
                    Contexto.Entry(tickets).State = EntityState.Deleted;
                    return Contexto.SaveChanges() > 0;
                }
                return false;

            }

            public Tickets? Buscar(int id)
            {
                return Contexto.Tickets.Where(o => o.TicketId == id).AsNoTracking().SingleOrDefault(); ;
            }

            public List<Tickets> GetList(Expression<Func<Tickets, bool>> criterio)
            {
                return Contexto.Tickets.AsNoTracking().Where(criterio).ToList();
            }

        }
    }
}
