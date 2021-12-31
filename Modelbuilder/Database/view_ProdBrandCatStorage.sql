CREATE VIEW view_ProdBrandCatStorage 
	AS SELECT p.id, p.name, b.name, c.name, s.name 
    FROM product p
		INNER JOIN brand b ON p.id = b.id
		INNER JOIN category c ON p.id = c.id
		INNER JOIN storage s ON p.id = s.id
	ORDER BY p.name