
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/16/2014 22:16:26
-- Generated from EDMX file: C:\Users\Enes\Desktop\Baze_podataka-projekat\GitHub\eOrdinacija\eOrdinacija\EOrdinacija-Baze\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BazaPodataka];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Acount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Acount];
GO
IF OBJECT_ID(N'[dbo].[kartoni]', 'U') IS NOT NULL
    DROP TABLE [dbo].[kartoni];
GO
IF OBJECT_ID(N'[dbo].[Korisnik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Korisnik];
GO
IF OBJECT_ID(N'[dbo].[Pacijenti]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pacijenti];
GO
IF OBJECT_ID(N'[dbo].[Prava]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prava];
GO
IF OBJECT_ID(N'[dbo].[Pregledi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pregledi];
GO
IF OBJECT_ID(N'[dbo].[Privilegije]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Privilegije];
GO
IF OBJECT_ID(N'[dbo].[Termini]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Termini];
GO
IF OBJECT_ID(N'[dbo].[Uposlenik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uposlenik];
GO
IF OBJECT_ID(N'[dbo].[Zaliha]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zaliha];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Acount'
CREATE TABLE [dbo].[Acount] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Korisnik_IdKorisnika] int  NOT NULL
);
GO

-- Creating table 'kartoni'
CREATE TABLE [dbo].[kartoni] (
    [IdKartona] int  NOT NULL,
    [IdDoktora] int  NOT NULL,
    [IdPacijenta] int  NOT NULL,
    [Ekg_slika] varbinary(max)  NULL,
    [Misljenje_specijaliste] varchar(max)  NULL,
    [Datum_zadnje_promjene] datetime  NOT NULL,
    [Dijagnoza] varchar(max)  NULL,
    [Primjedba] varchar(max)  NULL,
    [Korisnik_IdKorisnika] int  NOT NULL
);
GO

-- Creating table 'Korisnik'
CREATE TABLE [dbo].[Korisnik] (
    [IdKorisnika] int IDENTITY(1,1) NOT NULL,
    [Ime] varchar(50)  NOT NULL,
    [Ime_oca] varchar(50)  NOT NULL,
    [Prezime] varchar(50)  NOT NULL,
    [Datum_rodjenja] datetime  NOT NULL,
    [Mjesto_rodjenja] varchar(50)  NOT NULL,
    [Broj_licne_karte] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [IdPrivilegije] int  NOT NULL,
    [idKartona] int  NULL,
    [idAcount] int  NULL
);
GO

-- Creating table 'Pacijenti'
CREATE TABLE [dbo].[Pacijenti] (
    [IdPacijenta] int IDENTITY(1,1) NOT NULL,
    [IdKorisnika] int  NOT NULL,
    [KorisnikIdKorisnika] int  NOT NULL
);
GO

-- Creating table 'Prava'
CREATE TABLE [dbo].[Prava] (
    [IdPrava] int IDENTITY(1,1) NOT NULL,
    [Dodavanje_korisnika] bit  NOT NULL,
    [Dodavanje_doktora] bit  NOT NULL,
    [Izmjena_i_brianje_korisnika] bit  NOT NULL,
    [Izmjena_i_brisanje_doktora] bit  NOT NULL,
    [Dodavanje_privilegija_] bit  NOT NULL,
    [Pregled_kartona] bit  NOT NULL,
    [Zakazivanje_termina] bit  NOT NULL,
    [Privilegije_IdPrivilegije] int  NOT NULL
);
GO

-- Creating table 'Pregledi'
CREATE TABLE [dbo].[Pregledi] (
    [IdPregleda] int IDENTITY(1,1) NOT NULL,
    [Cjena] decimal(19,4)  NOT NULL,
    [Broj_kartona] int  NOT NULL,
    [idTermina] int  NOT NULL,
    [TerminiIdTermina] int  NOT NULL
);
GO

-- Creating table 'Privilegije'
CREATE TABLE [dbo].[Privilegije] (
    [IdPrivilegije] int IDENTITY(1,1) NOT NULL,
    [Naziv] nchar(10)  NOT NULL,
    [Opis] nchar(10)  NULL,
    [IdPrava] int  NOT NULL,
    [KorisnikIdKorisnika] int  NOT NULL
);
GO

-- Creating table 'Termini'
CREATE TABLE [dbo].[Termini] (
    [IdTermina] int IDENTITY(1,1) NOT NULL,
    [IdUposlenika] int  NOT NULL,
    [IdPacijenta] int  NOT NULL,
    [DatumTermina] datetime  NOT NULL,
    [Tip_pregleda] varchar(50)  NOT NULL,
    [PacijentiIdPacijenta] int  NOT NULL,
    [UposlenikIdUposlenika] int  NOT NULL
);
GO

-- Creating table 'Uposlenik'
CREATE TABLE [dbo].[Uposlenik] (
    [IdUposlenika] int  NOT NULL,
    [Titula] varchar(50)  NULL,
    [Zanimanje] varchar(50)  NOT NULL,
    [Specijalnost] varchar(50)  NULL,
    [Godina_zaposlenja] int  NOT NULL,
    [Plata] decimal(19,4)  NOT NULL,
    [IdKorisnika] int  NOT NULL,
    [KorisnikIdKorisnika] int  NOT NULL
);
GO

-- Creating table 'Zaliha'
CREATE TABLE [dbo].[Zaliha] (
    [IdZaliha] int  NOT NULL,
    [Ime_lijeka] varchar(50)  NOT NULL,
    [Proizvodjac] varchar(50)  NOT NULL,
    [Datum_isteka_roka] datetime  NOT NULL,
    [Cjena] decimal(19,4)  NOT NULL,
    [Kolicina] int  NOT NULL,
    [idPregleda] int  NOT NULL,
    [PreglediIdPregleda] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Acount'
ALTER TABLE [dbo].[Acount]
ADD CONSTRAINT [PK_Acount]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdKartona] in table 'kartoni'
ALTER TABLE [dbo].[kartoni]
ADD CONSTRAINT [PK_kartoni]
    PRIMARY KEY CLUSTERED ([IdKartona] ASC);
GO

-- Creating primary key on [IdKorisnika] in table 'Korisnik'
ALTER TABLE [dbo].[Korisnik]
ADD CONSTRAINT [PK_Korisnik]
    PRIMARY KEY CLUSTERED ([IdKorisnika] ASC);
GO

-- Creating primary key on [IdPacijenta] in table 'Pacijenti'
ALTER TABLE [dbo].[Pacijenti]
ADD CONSTRAINT [PK_Pacijenti]
    PRIMARY KEY CLUSTERED ([IdPacijenta] ASC);
GO

-- Creating primary key on [IdPrava] in table 'Prava'
ALTER TABLE [dbo].[Prava]
ADD CONSTRAINT [PK_Prava]
    PRIMARY KEY CLUSTERED ([IdPrava] ASC);
GO

-- Creating primary key on [IdPregleda] in table 'Pregledi'
ALTER TABLE [dbo].[Pregledi]
ADD CONSTRAINT [PK_Pregledi]
    PRIMARY KEY CLUSTERED ([IdPregleda] ASC);
GO

-- Creating primary key on [IdPrivilegije] in table 'Privilegije'
ALTER TABLE [dbo].[Privilegije]
ADD CONSTRAINT [PK_Privilegije]
    PRIMARY KEY CLUSTERED ([IdPrivilegije] ASC);
GO

-- Creating primary key on [IdTermina] in table 'Termini'
ALTER TABLE [dbo].[Termini]
ADD CONSTRAINT [PK_Termini]
    PRIMARY KEY CLUSTERED ([IdTermina] ASC);
GO

-- Creating primary key on [IdUposlenika] in table 'Uposlenik'
ALTER TABLE [dbo].[Uposlenik]
ADD CONSTRAINT [PK_Uposlenik]
    PRIMARY KEY CLUSTERED ([IdUposlenika] ASC);
GO

-- Creating primary key on [IdZaliha] in table 'Zaliha'
ALTER TABLE [dbo].[Zaliha]
ADD CONSTRAINT [PK_Zaliha]
    PRIMARY KEY CLUSTERED ([IdZaliha] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Korisnik_IdKorisnika] in table 'Acount'
ALTER TABLE [dbo].[Acount]
ADD CONSTRAINT [FK_AcountKorisnik]
    FOREIGN KEY ([Korisnik_IdKorisnika])
    REFERENCES [dbo].[Korisnik]
        ([IdKorisnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AcountKorisnik'
CREATE INDEX [IX_FK_AcountKorisnik]
ON [dbo].[Acount]
    ([Korisnik_IdKorisnika]);
GO

-- Creating foreign key on [Korisnik_IdKorisnika] in table 'kartoni'
ALTER TABLE [dbo].[kartoni]
ADD CONSTRAINT [FK_kartoniKorisnik]
    FOREIGN KEY ([Korisnik_IdKorisnika])
    REFERENCES [dbo].[Korisnik]
        ([IdKorisnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_kartoniKorisnik'
CREATE INDEX [IX_FK_kartoniKorisnik]
ON [dbo].[kartoni]
    ([Korisnik_IdKorisnika]);
GO

-- Creating foreign key on [Privilegije_IdPrivilegije] in table 'Prava'
ALTER TABLE [dbo].[Prava]
ADD CONSTRAINT [FK_PrivilegijePrava]
    FOREIGN KEY ([Privilegije_IdPrivilegije])
    REFERENCES [dbo].[Privilegije]
        ([IdPrivilegije])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrivilegijePrava'
CREATE INDEX [IX_FK_PrivilegijePrava]
ON [dbo].[Prava]
    ([Privilegije_IdPrivilegije]);
GO

-- Creating foreign key on [PreglediIdPregleda] in table 'Zaliha'
ALTER TABLE [dbo].[Zaliha]
ADD CONSTRAINT [FK_PreglediZaliha]
    FOREIGN KEY ([PreglediIdPregleda])
    REFERENCES [dbo].[Pregledi]
        ([IdPregleda])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PreglediZaliha'
CREATE INDEX [IX_FK_PreglediZaliha]
ON [dbo].[Zaliha]
    ([PreglediIdPregleda]);
GO

-- Creating foreign key on [TerminiIdTermina] in table 'Pregledi'
ALTER TABLE [dbo].[Pregledi]
ADD CONSTRAINT [FK_TerminiPregledi]
    FOREIGN KEY ([TerminiIdTermina])
    REFERENCES [dbo].[Termini]
        ([IdTermina])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TerminiPregledi'
CREATE INDEX [IX_FK_TerminiPregledi]
ON [dbo].[Pregledi]
    ([TerminiIdTermina]);
GO

-- Creating foreign key on [PacijentiIdPacijenta] in table 'Termini'
ALTER TABLE [dbo].[Termini]
ADD CONSTRAINT [FK_PacijentiTermini]
    FOREIGN KEY ([PacijentiIdPacijenta])
    REFERENCES [dbo].[Pacijenti]
        ([IdPacijenta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PacijentiTermini'
CREATE INDEX [IX_FK_PacijentiTermini]
ON [dbo].[Termini]
    ([PacijentiIdPacijenta]);
GO

-- Creating foreign key on [UposlenikIdUposlenika] in table 'Termini'
ALTER TABLE [dbo].[Termini]
ADD CONSTRAINT [FK_UposlenikTermini]
    FOREIGN KEY ([UposlenikIdUposlenika])
    REFERENCES [dbo].[Uposlenik]
        ([IdUposlenika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UposlenikTermini'
CREATE INDEX [IX_FK_UposlenikTermini]
ON [dbo].[Termini]
    ([UposlenikIdUposlenika]);
GO

-- Creating foreign key on [KorisnikIdKorisnika] in table 'Pacijenti'
ALTER TABLE [dbo].[Pacijenti]
ADD CONSTRAINT [FK_KorisnikPacijenti]
    FOREIGN KEY ([KorisnikIdKorisnika])
    REFERENCES [dbo].[Korisnik]
        ([IdKorisnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KorisnikPacijenti'
CREATE INDEX [IX_FK_KorisnikPacijenti]
ON [dbo].[Pacijenti]
    ([KorisnikIdKorisnika]);
GO

-- Creating foreign key on [KorisnikIdKorisnika] in table 'Uposlenik'
ALTER TABLE [dbo].[Uposlenik]
ADD CONSTRAINT [FK_KorisnikUposlenik]
    FOREIGN KEY ([KorisnikIdKorisnika])
    REFERENCES [dbo].[Korisnik]
        ([IdKorisnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KorisnikUposlenik'
CREATE INDEX [IX_FK_KorisnikUposlenik]
ON [dbo].[Uposlenik]
    ([KorisnikIdKorisnika]);
GO

-- Creating foreign key on [KorisnikIdKorisnika] in table 'Privilegije'
ALTER TABLE [dbo].[Privilegije]
ADD CONSTRAINT [FK_KorisnikPrivilegije]
    FOREIGN KEY ([KorisnikIdKorisnika])
    REFERENCES [dbo].[Korisnik]
        ([IdKorisnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KorisnikPrivilegije'
CREATE INDEX [IX_FK_KorisnikPrivilegije]
ON [dbo].[Privilegije]
    ([KorisnikIdKorisnika]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------