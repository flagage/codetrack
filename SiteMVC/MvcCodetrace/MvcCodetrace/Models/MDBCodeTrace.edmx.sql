
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2015 11:39:38
-- Generated from EDMX file: C:\Users\PLOQUIN\Documents\Visual Studio 2013\Projects\SiteMVC\MvcCodetrace\MvcCodetrace\Models\MDBCodeTrace.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeTrace];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CodeAgregation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Agregations] DROP CONSTRAINT [FK_CodeAgregation];
GO
IF OBJECT_ID(N'[dbo].[FK_Bouteille_inherits_Code]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bouteilles] DROP CONSTRAINT [FK_Bouteille_inherits_Code];
GO
IF OBJECT_ID(N'[dbo].[FK_RengementBouteille]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bouteilles] DROP CONSTRAINT [FK_RengementBouteille];
GO
IF OBJECT_ID(N'[dbo].[FK_Cave_inherits_Code]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Caves] DROP CONSTRAINT [FK_Cave_inherits_Code];
GO
IF OBJECT_ID(N'[dbo].[FK_CaveRengement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rengements] DROP CONSTRAINT [FK_CaveRengement];
GO
IF OBJECT_ID(N'[dbo].[FK_Rengement_inherits_Code]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rengements] DROP CONSTRAINT [FK_Rengement_inherits_Code];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Agregations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agregations];
GO
IF OBJECT_ID(N'[dbo].[Bouteilles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bouteilles];
GO
IF OBJECT_ID(N'[dbo].[Caves]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Caves];
GO
IF OBJECT_ID(N'[dbo].[Codes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Codes];
GO
IF OBJECT_ID(N'[dbo].[Rengements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rengements];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Agregations'
CREATE TABLE [dbo].[Agregations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodeContenu] nvarchar(max)  NOT NULL,
    [CodeId] int  NULL
);
GO

-- Creating table 'Bouteilles'
CREATE TABLE [dbo].[Bouteilles] (
    [TypeVin] nvarchar(max)  NOT NULL,
    [Region] nvarchar(max)  NOT NULL,
    [Producteur] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RengementId] int  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Caves'
CREATE TABLE [dbo].[Caves] (
    [Proprietaire] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Codes'
CREATE TABLE [dbo].[Codes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodeTrace] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rengements'
CREATE TABLE [dbo].[Rengements] (
    [Emplacement] nvarchar(max)  NOT NULL,
    [QuantiteMax] nvarchar(max)  NOT NULL,
    [Quantite] nvarchar(max)  NOT NULL,
    [CaveId] int  NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Agregations'
ALTER TABLE [dbo].[Agregations]
ADD CONSTRAINT [PK_Agregations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bouteilles'
ALTER TABLE [dbo].[Bouteilles]
ADD CONSTRAINT [PK_Bouteilles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Caves'
ALTER TABLE [dbo].[Caves]
ADD CONSTRAINT [PK_Caves]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Codes'
ALTER TABLE [dbo].[Codes]
ADD CONSTRAINT [PK_Codes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rengements'
ALTER TABLE [dbo].[Rengements]
ADD CONSTRAINT [PK_Rengements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CodeId] in table 'Agregations'
ALTER TABLE [dbo].[Agregations]
ADD CONSTRAINT [FK_CodeAgregation]
    FOREIGN KEY ([CodeId])
    REFERENCES [dbo].[Codes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeAgregation'
CREATE INDEX [IX_FK_CodeAgregation]
ON [dbo].[Agregations]
    ([CodeId]);
GO

-- Creating foreign key on [Id] in table 'Bouteilles'
ALTER TABLE [dbo].[Bouteilles]
ADD CONSTRAINT [FK_Bouteille_inherits_Code]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Codes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RengementId] in table 'Bouteilles'
ALTER TABLE [dbo].[Bouteilles]
ADD CONSTRAINT [FK_RengementBouteille]
    FOREIGN KEY ([RengementId])
    REFERENCES [dbo].[Rengements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RengementBouteille'
CREATE INDEX [IX_FK_RengementBouteille]
ON [dbo].[Bouteilles]
    ([RengementId]);
GO

-- Creating foreign key on [Id] in table 'Caves'
ALTER TABLE [dbo].[Caves]
ADD CONSTRAINT [FK_Cave_inherits_Code]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Codes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CaveId] in table 'Rengements'
ALTER TABLE [dbo].[Rengements]
ADD CONSTRAINT [FK_CaveRengement]
    FOREIGN KEY ([CaveId])
    REFERENCES [dbo].[Caves]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CaveRengement'
CREATE INDEX [IX_FK_CaveRengement]
ON [dbo].[Rengements]
    ([CaveId]);
GO

-- Creating foreign key on [Id] in table 'Rengements'
ALTER TABLE [dbo].[Rengements]
ADD CONSTRAINT [FK_Rengement_inherits_Code]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Codes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------