.pkgname <- "genome.rd7"
.seqnames <- paste("chr", c(1:22, "X", "Y"), sep = "")
.circ_seqs <- "chrD"
.mseqnames <- NULL

.onLoad <- function(libname, pkgname) {
	if (pkgname != .pkgname)
		stop("package name (", pkgname, ") is not ", "the expected name (", .pkgname, ")")
	extdata_dirpath <- system.file("gendata", package = pkgname, lib.loc = libname, mustWork = TRUE)
	bsgenome <- BSgenome(
		organism = "Unknown",
		species = "unk",
		provider = "UCSC",
		provider_version = "hg19",
		release_date = "July 2019",
		release_name = "Genome",
		seqnames = .seqnames,
		circ_seqs = .circ_seqs,
		mseqnames = .mseqnames,
		seqs_pkgname = pkgname,
		seqs_dirpath = extdata_dirpath
	)
	ns <- asNamespace(pkgname)

	objname <- pkgname
	assign(objname, bsgenome, envir = ns)
	namespaceExport(ns, objname)
}
