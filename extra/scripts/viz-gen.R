library(ggbio)
library(GenomicRanges)
p <- ggbio()
data("CRC", package = "biovizBase")
head(hg19sub)
p <- ggbio() + circle(hg19sub, geom = "ideo", fill = "gray70") +
	+ circle(hg19sub, geom = "scale", size = 2) +
	+ circle(hg19sub, geom = "text", aes(label = seqnames), vjust = 0, size = 3)
p