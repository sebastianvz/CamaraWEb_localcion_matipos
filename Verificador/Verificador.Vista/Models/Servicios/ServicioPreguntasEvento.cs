using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioPreguntasEvento : IDisposable
    {
        private Entities entities;

        public ServicioPreguntasEvento()
        {
            this.entities = new Entities();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        public string Create(List<PreguntaViewModel> obj, int idEvento)
        {
            var mensaje = "";
            try
            {
                foreach (var item in obj)
                {
                    var entityPregunta = new PreguntasEvento();
                    entityPregunta.IdEvento = idEvento;
                    entityPregunta.NumeroPregunta = item.NumeroPregunta;
                    entityPregunta.TextoPregunta = item.TextoPregunta;
                    entityPregunta.TipoRespuesta = item.TipoRespuesta;
                    entities.Add(entityPregunta);
                    entities.SaveChanges();
                    var id = entityPregunta.Id;

                    foreach (var itemOpciones in item.Opciones)
                    {
                        var entityOpciones = new OpcionesPreguntaEvento();
                        entityOpciones.IdPregunta = id;
                        entityOpciones.NumeroOpcion = itemOpciones.NumeroOpcion;
                        entityOpciones.TextoOpcion = itemOpciones.TextoOpcion;
                        entities.Add(entityOpciones);
                        entities.SaveChanges();
                    }

                }

                mensaje = "OK";

            }
            catch (Exception ex)
            {
                mensaje = ex.Source + ex.Message;
            }

            return (mensaje);
        }

        public List<PreguntaViewModel> ReadbyiD(int id)
        {
            List<PreguntaViewModel> Lista = entities.PreguntasEventos
                .Where(c => c.IdEvento == id)
                .Select(c => new PreguntaViewModel
                {
                    Id = c.Id,
                    IdEvento = c.IdEvento,
                    NumeroPregunta = c.NumeroPregunta,
                    TextoPregunta = c.TextoPregunta,
                    TipoRespuesta = c.TipoRespuesta,
                    Opciones = entities.OpcionesPreguntaEventos
                    .Where(o => c.Id == o.IdPregunta)
                    .Select(o => new OpcionesPreguntaViewModel
                    {
                        Id = o.Id,
                        IdPregunta = o.IdPregunta,
                        NumeroOpcion = o.NumeroOpcion,
                        TextoOpcion = o.TextoOpcion
                    }).ToList()
                }).ToList();

            return Lista;
        }

    }


}