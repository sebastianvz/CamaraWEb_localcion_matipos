USE [verificador]
GO
/****** Object:  Table [dbo].[CanalesVenta]    Script Date: 16/09/2018 3:54:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanalesVenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[canalesVentaEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[canalesVentaEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idcanal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categorias]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categorias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](200) NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[verCodigoArtesEscenicas] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categoriasEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoriasEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEvento] [int] NULL,
	[idCategoria] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ciudades]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ciudades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[iddepartamento] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[clientes]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoPersona_id] [int] NOT NULL,
	[tipoDocumento_id] [int] NOT NULL,
	[identificacion] [varchar](100) NOT NULL,
	[primerNombre] [varchar](200) NOT NULL,
	[segundoNombre] [varchar](200) NULL,
	[primerApellido] [varchar](200) NOT NULL,
	[segundoApellido] [varchar](200) NULL,
	[fechaNacimiento] [smalldatetime] NOT NULL,
	[genero_id] [int] NOT NULL,
	[email] [varchar](200) NOT NULL,
	[celular] [varchar](100) NOT NULL,
	[telefonoCasa] [varchar](100) NULL,
	[telefonoOficina] [varchar](100) NULL,
	[jornadaEntrega] [varchar](100) NULL,
	[idCreatorby] [int] NULL,
	[dateCreated] [date] NULL,
	[idRetiredby] [int] NULL,
	[dateRetired] [date] NULL,
	[idChangedby] [int] NULL,
	[dateChanged] [date] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK__clientes__3213E83F35A17576] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[consumo]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[consumo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [money] NULL,
	[descripcion] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[departamentos]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departamentos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[idpais] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[direccionesClientes]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direccionesClientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ubicacion] [varchar](500) NOT NULL,
	[codigoPostal] [varchar](50) NULL,
	[ciudad_id] [int] NOT NULL,
	[cliente_id] [int] NOT NULL,
	[idCreatorby] [int] NULL,
	[dateCreated] [date] NULL,
	[idRetiredby] [int] NULL,
	[dateRetired] [date] NULL,
	[idChangedby] [int] NULL,
	[dateChanged] [date] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK__direccio__3213E83F29A0899F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[empresarios]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empresarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoEmpresa_id] [int] NULL,
	[razonSocial] [varchar](200) NULL,
	[tipoDocumento_id] [int] NULL,
	[identificacion] [varchar](100) NULL,
	[tipoPersona_id] [int] NULL,
	[direccion] [varchar](300) NULL,
	[ciudad_id] [int] NULL,
	[telefono] [varchar](100) NULL,
	[celular] [varchar](100) NULL,
	[emailNotificacion] [varchar](200) NULL,
	[numeroRegistroArtesEscenicas] [varchar](100) NULL,
	[nombreRL] [varchar](200) NULL,
	[apellidosRL] [varchar](200) NULL,
	[tipoDocumentoRL_id] [int] NULL,
	[identificacionRL] [varchar](100) NULL,
	[sexo_idRL] [int] NULL,
	[emailRL] [varchar](200) NULL,
	[direccionRL] [varchar](300) NULL,
	[ciudad_idRL] [int] NULL,
	[telefonoRL] [varchar](100) NULL,
	[celularRL] [varchar](100) NULL,
	[ventasWebReacudoDirecto] [bit] NULL,
	[crateDate] [smalldatetime] NULL,
	[bloqueado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EstadoImpresion]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[etapasVentaEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[etapasVentaEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[descripcion] [varchar](500) NULL,
	[tipo] [varchar](20) NULL,
	[fechaInicial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[idevento] [int] NULL,
	[LlevaCodigo] [bit] NULL,
	[Codigo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[eventos]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eventos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[multifuncion] [bit] NULL,
	[cantidadFunciones] [int] NULL,
	[fechaInicial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[empresario_id] [int] NULL,
	[tipoEvento] [int] NULL,
	[foto] [nvarchar](100) NULL,
	[descripcion] [nvarchar](max) NULL,
	[edadMinima] [int] NULL,
	[eventoPublico] [bit] NULL,
	[tipoRecaudo_id] [int] NULL,
	[entradasxPedidoOcompra] [int] NULL,
	[entradasxUsuario] [int] NULL,
	[aplicaServicio] [bit] NULL,
	[cantidadDiasValides] [int] NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[AforoTotal] [int] NULL,
	[Estado] [varchar](20) NULL,
	[urlVideo] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[funcionesEventos]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[funcionesEventos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[fechaIncial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[descripcion] [varchar](1000) NULL,
	[label] [varchar](50) NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[aperturaPuertas] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Genero]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Impuesto]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impuesto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[valor] [decimal](18, 3) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[impuestosEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[impuestosEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idimpuesto] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[localidad]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[localidad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Aforo] [int] NULL,
	[IdTipoLocalidad] [int] NULL,
	[FechaCreacion] [smalldatetime] NULL,
	[TicketNumeroInicio] [int] NULL,
	[TicketNumeroFinal] [int] NULL,
	[RangoTickets] [bit] NULL,
	[IdTipoNumeracion] [int] NULL,
	[Consumo] [varchar](100) NULL,
	[ValorConsumo] [money] NULL,
	[Filas] [int] NULL,
	[Sillas] [int] NULL,
	[OrientacionFilas] [varchar](20) NULL,
	[OrientacionSillas] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalidadesVenueEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalidadesVenueEvento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVenue] [int] NULL,
	[IdLocalidad] [int] NULL,
	[IdEvento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogPrintTicket]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogPrintTicket](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Estado] [varchar](50) NULL,
	[IdTransaccioTicketImpresion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mapas]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mapas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](100) NULL,
	[descripcion] [varchar](500) NULL,
	[capacidad] [int] NULL,
	[mapa] [nvarchar](1000) NULL,
	[fechaCreacion] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[mapasVenue]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mapasVenue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idVenue] [int] NULL,
	[idMapa] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[paises]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[permisos]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permisos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[descripcion] [varchar](200) NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[permisos_rol]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permisos_rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idpermiso] [int] NOT NULL,
	[idrol] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrintersByComputers]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintersByComputers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MacAddress] [nvarchar](200) NULL,
	[PrinterName] [varchar](100) NULL,
	[TicketType] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roles]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roles_usuario]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles_usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idrol] [int] NOT NULL,
	[idusuario] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[serviciosEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[serviciosEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idservicio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tickets]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tickets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NULL,
	[barcode] [nvarchar](max) NULL,
	[numericBarcode] [varchar](50) NULL,
	[idEstado] [int] NULL,
	[fechaImpresion] [smalldatetime] NULL,
	[idTicketsEvento] [int] NULL,
	[idTicketsEtapa] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticketsEtapa]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticketsEtapa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idLocalidad] [int] NOT NULL,
	[idEtapa] [int] NOT NULL,
	[IdTipoServicio] [int] NULL,
	[Aforo] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_ticketsEtapa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticketsEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticketsEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEvento] [int] NULL,
	[cantidad] [int] NULL,
	[inicioNumeracion] [int] NULL,
	[JsonCanvas] [nvarchar](max) NULL,
	[finNumeracion] [int] NULL,
	[ticketType] [varchar](15) NULL,
	[ticketImage] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipodocumento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipodocumento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoempresa]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoempresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoEvento]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoLocalidad]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoLocalidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](200) NULL,
	[fechaCreacion] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipomoneda]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipomoneda](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](10) NOT NULL,
	[simbolo] [varchar](10) NOT NULL,
	[decimales] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoNumeracion]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoNumeracion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numeracion] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipopago]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipopago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipopersona]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipopersona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoRecaudo]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoRecaudo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoServicio]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoServicio](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoUsuario]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](100) NULL,
	[description] [varchar](200) NULL,
	[createDate] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[transaccionPullTicketsImpresion]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaccionPullTicketsImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [smalldatetime] NULL,
	[Token] [nvarchar](200) NULL,
	[ImagenTicket] [nvarchar](max) NULL,
	[idTicket] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[transaccionTicketsImpresion]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaccionTicketsImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTicket] [int] NULL,
	[token] [nvarchar](500) NULL,
	[fechaImpresion] [smalldatetime] NULL,
	[idEstado] [int] NULL,
	[idTicketEvento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[contrasena] [varchar](80) NOT NULL,
	[bloqueado] [bit] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[nombres] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[direccion] [varchar](200) NULL,
	[telefono] [varchar](30) NULL,
	[fecha] [datetime] NULL,
	[fechacreacion] [datetime] NOT NULL,
	[tipoUsuario_id] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuarios_portal]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios_portal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contrasena] [varchar](80) NOT NULL,
	[bloqueado] [bit] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[nombres] [varchar](80) NULL,
	[apellidos] [varchar](50) NULL,
	[fecha] [datetime] NULL,
	[fechacreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_usuarios_portal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UVT]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UVT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[anio] [int] NOT NULL,
	[valor] [decimal](18, 3) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venue]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[direccion] [varchar](500) NULL,
	[ciudad_id] [int] NULL,
	[telefono] [varchar](100) NULL,
	[email] [varchar](200) NULL,
	[foto] [nvarchar](1000) NULL,
	[aforo] [int] NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[Latitud] [varchar](100) NULL,
	[Longitud] [varchar](100) NULL,
	[CityName] [varchar](50) NULL,
	[DepartametName] [varchar](50) NULL,
	[CountryName] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[GeoCode] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CanalesVenta] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[ciudades] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[departamentos] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[EstadoImpresion] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[Genero] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[Impuesto] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[paises] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[permisos] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[permisos_rol] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[roles] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[roles_usuario] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[tipodocumento] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[tipoempresa] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[TipoEvento] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[tipomoneda] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[tipopago] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[tipopersona] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[TipoRecaudo] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[TipoServicio] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[usuarios] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[usuarios_portal] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[UVT] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[canalesVentaEvento]  WITH CHECK ADD FOREIGN KEY([idcanal])
REFERENCES [dbo].[CanalesVenta] ([id])
GO
ALTER TABLE [dbo].[canalesVentaEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[categoriasEvento]  WITH CHECK ADD FOREIGN KEY([idEvento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[categoriasEvento]  WITH CHECK ADD  CONSTRAINT [FK_categoriasEvento_categorias] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categorias] ([id])
GO
ALTER TABLE [dbo].[categoriasEvento] CHECK CONSTRAINT [FK_categoriasEvento_categorias]
GO
ALTER TABLE [dbo].[ciudades]  WITH CHECK ADD  CONSTRAINT [FK_Ciudades_Departamentos] FOREIGN KEY([iddepartamento])
REFERENCES [dbo].[departamentos] ([id])
GO
ALTER TABLE [dbo].[ciudades] CHECK CONSTRAINT [FK_Ciudades_Departamentos]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__genero__06CD04F7] FOREIGN KEY([genero_id])
REFERENCES [dbo].[Genero] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__genero__06CD04F7]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__tipoDo__07C12930] FOREIGN KEY([tipoDocumento_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__tipoDo__07C12930]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__tipoPe__08B54D69] FOREIGN KEY([tipoPersona_id])
REFERENCES [dbo].[tipopersona] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__tipoPe__08B54D69]
GO
ALTER TABLE [dbo].[departamentos]  WITH CHECK ADD  CONSTRAINT [FK_Departamentos_Paises] FOREIGN KEY([idpais])
REFERENCES [dbo].[paises] ([id])
GO
ALTER TABLE [dbo].[departamentos] CHECK CONSTRAINT [FK_Departamentos_Paises]
GO
ALTER TABLE [dbo].[direccionesClientes]  WITH CHECK ADD  CONSTRAINT [FK__direccion__ciuda__0A9D95DB] FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[direccionesClientes] CHECK CONSTRAINT [FK__direccion__ciuda__0A9D95DB]
GO
ALTER TABLE [dbo].[direccionesClientes]  WITH CHECK ADD  CONSTRAINT [FK__direccion__clien__0B91BA14] FOREIGN KEY([cliente_id])
REFERENCES [dbo].[clientes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[direccionesClientes] CHECK CONSTRAINT [FK__direccion__clien__0B91BA14]
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([ciudad_idRL])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([sexo_idRL])
REFERENCES [dbo].[Genero] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoDocumentoRL_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoDocumento_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoEmpresa_id])
REFERENCES [dbo].[tipoempresa] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoPersona_id])
REFERENCES [dbo].[tipopersona] ([id])
GO
ALTER TABLE [dbo].[etapasVentaEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([empresario_id])
REFERENCES [dbo].[empresarios] ([id])
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([tipoEvento])
REFERENCES [dbo].[TipoEvento] ([id])
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([tipoRecaudo_id])
REFERENCES [dbo].[TipoRecaudo] ([id])
GO
ALTER TABLE [dbo].[funcionesEventos]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[impuestosEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[impuestosEvento]  WITH CHECK ADD FOREIGN KEY([idimpuesto])
REFERENCES [dbo].[Impuesto] ([id])
GO
ALTER TABLE [dbo].[localidad]  WITH CHECK ADD FOREIGN KEY([IdTipoNumeracion])
REFERENCES [dbo].[tipoNumeracion] ([Id])
GO
ALTER TABLE [dbo].[localidad]  WITH CHECK ADD FOREIGN KEY([IdTipoLocalidad])
REFERENCES [dbo].[tipoLocalidad] ([id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdEvento])
REFERENCES [dbo].[eventos] ([id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdLocalidad])
REFERENCES [dbo].[localidad] ([Id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdVenue])
REFERENCES [dbo].[venue] ([id])
GO
ALTER TABLE [dbo].[LogPrintTicket]  WITH CHECK ADD FOREIGN KEY([IdTransaccioTicketImpresion])
REFERENCES [dbo].[transaccionTicketsImpresion] ([id])
GO
ALTER TABLE [dbo].[mapasVenue]  WITH CHECK ADD FOREIGN KEY([idMapa])
REFERENCES [dbo].[mapas] ([id])
GO
ALTER TABLE [dbo].[mapasVenue]  WITH CHECK ADD FOREIGN KEY([idVenue])
REFERENCES [dbo].[venue] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[permisos_rol]  WITH CHECK ADD  CONSTRAINT [fk_permisos_rol_permisos] FOREIGN KEY([idpermiso])
REFERENCES [dbo].[permisos] ([id])
GO
ALTER TABLE [dbo].[permisos_rol] CHECK CONSTRAINT [fk_permisos_rol_permisos]
GO
ALTER TABLE [dbo].[permisos_rol]  WITH CHECK ADD  CONSTRAINT [fk_permisos_rol_roles] FOREIGN KEY([idrol])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[permisos_rol] CHECK CONSTRAINT [fk_permisos_rol_roles]
GO
ALTER TABLE [dbo].[roles_usuario]  WITH CHECK ADD  CONSTRAINT [fk_roles_usuario_roles] FOREIGN KEY([idrol])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[roles_usuario] CHECK CONSTRAINT [fk_roles_usuario_roles]
GO
ALTER TABLE [dbo].[roles_usuario]  WITH CHECK ADD  CONSTRAINT [fk_roles_usuario_usuarios] FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[roles_usuario] CHECK CONSTRAINT [fk_roles_usuario_usuarios]
GO
ALTER TABLE [dbo].[serviciosEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[serviciosEvento]  WITH CHECK ADD FOREIGN KEY([idservicio])
REFERENCES [dbo].[TipoServicio] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD  CONSTRAINT [FK_tickets_ticketsEtapa] FOREIGN KEY([idTicketsEtapa])
REFERENCES [dbo].[ticketsEtapa] ([id])
GO
ALTER TABLE [dbo].[tickets] CHECK CONSTRAINT [FK_tickets_ticketsEtapa]
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD  CONSTRAINT [FK_tickets_ticketsEvento] FOREIGN KEY([idTicketsEvento])
REFERENCES [dbo].[ticketsEvento] ([id])
GO
ALTER TABLE [dbo].[tickets] CHECK CONSTRAINT [FK_tickets_ticketsEvento]
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD FOREIGN KEY([IdTipoServicio])
REFERENCES [dbo].[TipoServicio] ([id])
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD  CONSTRAINT [FK_ticketsEtapa_etapasVentaEvento] FOREIGN KEY([idEtapa])
REFERENCES [dbo].[etapasVentaEvento] ([id])
GO
ALTER TABLE [dbo].[ticketsEtapa] CHECK CONSTRAINT [FK_ticketsEtapa_etapasVentaEvento]
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD  CONSTRAINT [FK_ticketsEtapa_localidad] FOREIGN KEY([idLocalidad])
REFERENCES [dbo].[localidad] ([Id])
GO
ALTER TABLE [dbo].[ticketsEtapa] CHECK CONSTRAINT [FK_ticketsEtapa_localidad]
GO
ALTER TABLE [dbo].[ticketsEvento]  WITH CHECK ADD FOREIGN KEY([idEvento])
REFERENCES [dbo].[eventos] ([id])
GO
ALTER TABLE [dbo].[transaccionPullTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idTicket])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idTicket])
REFERENCES [dbo].[tickets] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD  CONSTRAINT [FK_transaccionTicketsImpresion_ticketsEvento] FOREIGN KEY([idTicketEvento])
REFERENCES [dbo].[ticketsEvento] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion] CHECK CONSTRAINT [FK_transaccionTicketsImpresion_ticketsEvento]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD FOREIGN KEY([tipoUsuario_id])
REFERENCES [dbo].[tipoUsuario] ([id])
GO
ALTER TABLE [dbo].[venue]  WITH CHECK ADD FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
/****** Object:  StoredProcedure [dbo].[sp_ResetLocalidad]    Script Date: 16/09/2018 3:54:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_ResetLocalidad](
	@LocalidadID int
) as begin
	delete from sillas where id in (select idSilla from mesasFilasSillas where idMesasFilas in (select id from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID)))
	delete from mesasFilasSillas where idMesasFilas in (select id from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID))
	delete from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID)
	delete from localidadMesasFilasSillas where idLocalidad = @LocalidadID
end


GO
