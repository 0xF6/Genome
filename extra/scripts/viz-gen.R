library(ggbio)
library(GenomicRanges)
p <- ggbio()
data("CRC", package = "biovizBase")
head(hg18sub)
p <- ggbio() + circle(hg19sub, geom = "ideo", fill = "yellow") +
	 circle(hg19sub, geom = "scale", size = 3) +
	 circle(hg19sub, geom = "text", aes(label = seqnames), vjust = 10, size = 3)
p